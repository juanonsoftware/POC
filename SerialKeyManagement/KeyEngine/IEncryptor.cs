namespace KeyEngine
{
    public interface IEncryptor
    {
        string Encrypt(string plaintext);

        string Decrypt(string encryptedText);
    }
}