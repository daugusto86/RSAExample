// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text;

using var rsa = new RSACryptoServiceProvider(2048);
rsa.PersistKeyInCsp = false;

var publicKeyXml = rsa.ToXmlString(false);
var privateKeyXml = rsa.ToXmlString(true);

Console.WriteLine($"Chave pública: \n {publicKeyXml}{Environment.NewLine}");
Console.WriteLine($"Chave privada:\n {privateKeyXml}{Environment.NewLine}");

var message = "Mensagem criptografada";
Console.WriteLine($"Mensagem original: {message}{Environment.NewLine}");

byte[] encryptedMessage = Encrypt(Encoding.UTF8.GetBytes(message), publicKeyXml);
Console.WriteLine($"Mensagem criptografada (em Base64): {Convert.ToBase64String(encryptedMessage)}{Environment.NewLine}");

byte[] decryptedMessage = Decrypt(encryptedMessage, privateKeyXml);
Console.WriteLine($"Mensagem descriptografada: {Encoding.UTF8.GetString(decryptedMessage)}{Environment.NewLine}");

static byte[] Encrypt(byte[] dataToEncrypt, string publicKeyXml)
{
    using var rsa = new RSACryptoServiceProvider(2048);
    rsa.FromXmlString(publicKeyXml);
    
    return rsa.Encrypt(dataToEncrypt, true);
}

static byte[] Decrypt(byte[] dataToDecrypt, string privateKeyXml)
{
    using var rsa = new RSACryptoServiceProvider(2048);
    rsa.FromXmlString(privateKeyXml);
    
    return rsa.Decrypt(dataToDecrypt, true);
}