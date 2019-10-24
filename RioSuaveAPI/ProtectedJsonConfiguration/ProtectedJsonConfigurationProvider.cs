using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration.Json;
using UtilityLib;

namespace RioSuaveAPI.ProtectedJsonConfiguration
{
    public class ProtectedJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly JsonConfigurationSource _cfgSrc;

        public ProtectedJsonConfigurationProvider(JsonConfigurationSource cfgSrc) : base(cfgSrc)
        {
            _cfgSrc = cfgSrc;
        }

        public override void Load()
        {

            var key = GetEnvironmentVariable("APP_KEY");
            var iv = GetEnvironmentVariable("APP_IV");

            var aes = new Aes(key, iv);

            var encryptedBuffer = GetEncryptedBufferFromFile();
            var decrypted = aes.Decrypt(encryptedBuffer);

            base.Load(new MemoryStream(decrypted.ToArray()));
        }

        private byte[] GetEncryptedBufferFromFile()
        {
            var fStream = _cfgSrc.FileProvider.GetFileInfo(_cfgSrc.Path).CreateReadStream();

            var buffer = new byte[fStream.Length];
            fStream.Read(buffer);

            return buffer;
        }

        private static byte[] GetEnvironmentVariable(string env)
        {
            var base64Var = Environment.GetEnvironmentVariable(env, EnvironmentVariableTarget.User);
            return Convert.FromBase64String(base64Var);
        }
    }
}