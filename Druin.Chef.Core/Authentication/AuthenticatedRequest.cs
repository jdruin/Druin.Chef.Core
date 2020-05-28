using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace Druin.Chef.Core.Authentication
{
    public class AuthenticatedRequest
    {
        private readonly string client;
        private readonly Uri requestUri;
        private readonly HttpMethod method;
        private readonly string body;
        private readonly byte[] file;


        private readonly string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        private string signature = String.Empty;

        public AuthenticatedRequest(string client, Uri requestUri)
            : this(client, requestUri, HttpMethod.Get, String.Empty)
        {
        }

        public AuthenticatedRequest(string client, Uri requestUri, HttpMethod method, byte[] file)
        {
            this.client = client;
            this.requestUri = requestUri;
            this.method = method;
            this.file = file;
            this.body = String.Empty;
        }
        public AuthenticatedRequest(string client, Uri requestUri, HttpMethod method, string body)
        {
            this.client = client;
            this.requestUri = requestUri;
            this.method = method;
            this.body = body;
        }

        public void Sign(string privateKey)
        {
            string canonicalHeader =
                String.Format(
                    "Method:{0}\nHashed Path:{1}\nX-Ops-Content-Hash:{4}\nX-Ops-Timestamp:{3}\nX-Ops-UserId:{2}",
                    method,
                    requestUri.AbsolutePath.ToBase64EncodedSha1String(),
                    client,
                    timestamp,
                    body.ToBase64EncodedSha1String());

            byte[] input = Encoding.UTF8.GetBytes(canonicalHeader);

            var pemReader = new PemReader(new StringReader(privateKey));
            var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();

            var pkcs1Encoding = new Pkcs1Encoding(new RsaBlindedEngine());
            pkcs1Encoding.Init(true, keyPair.Private);
            var signer = pkcs1Encoding.ProcessBlock(input, 0, input.Length);

            signature = Convert.ToBase64String(signer);
        }

        public HttpRequestMessage Create()
        {
            var requestMessage = new HttpRequestMessage(method, requestUri);

            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("X-Ops-Sign", "algorithm=sha1;version=1.0");
            requestMessage.Headers.Add("X-Ops-UserId", client);
            requestMessage.Headers.Add("X-Ops-Timestamp", timestamp);
            requestMessage.Headers.Add("Host", String.Format("{0}:{1}", requestUri.Host, requestUri.Port));
            requestMessage.Headers.Add("X-Chef-Version", "12.14.89");
            requestMessage.Headers.Add("X-Ops-Server-API-Version", "0");

            if (method != HttpMethod.Get)
            {
                if (file != null)
                {
                    // This header is derived from mixlib/authentication/signedheaderauth/hashed body which leads to digester.rb
                    requestMessage.Headers.Add("X-Ops-Content-Hash", file.ToBase64EncodedSha1String());

                    // Pulled this out of knife run on -VV
                    requestMessage.Headers.Add("Accept-Encoding", "gzip; q = 1.0,deflate; q = 0.6,identity; q = 0.3");

                    requestMessage.Content = new ByteArrayContent(file);
                    // This header is from the notes in chef/cookbook_uploader/uploader_function_for
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-binary");

                    requestMessage.Content.Headers.ContentMD5 = file.ToMD5Hash();


                }
                else
                {
                    requestMessage.Content = new ByteArrayContent(Encoding.UTF8.GetBytes(body));
                    requestMessage.Content.Headers.Add("Content-Type", "application/json");
                    requestMessage.Headers.Add("X-Ops-Content-Hash", body.ToBase64EncodedSha1String());

                }

            }
            else
            {
                requestMessage.Headers.Add("X-Ops-Content-Hash", body.ToBase64EncodedSha1String());
            }



            var i = 1;
            foreach (var line in signature.Split(60))
            {
                requestMessage.Headers.Add(String.Format("X-Ops-Authorization-{0}", i++), line);
            }

            return requestMessage;
        }
    }
}