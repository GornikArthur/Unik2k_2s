using System.Text;

static uint Polynomial(char[] lfsr)
{
	uint bit26 = (uint)(lfsr[0] - '0');
	uint bit7 = (uint)(lfsr[19] - '0');
	uint bit6 = (uint)(lfsr[20] - '0');
	uint bit0 = (uint)(lfsr[26] - '0');

	return (bit26 ^ bit7 ^ bit6 ^ bit0);
}

static (byte[], string) ciphre(char[] lfsr, byte[] messageBytes)
{
	StringBuilder shiftedBits = new StringBuilder();
	byte[] crypted = new byte[messageBytes.Length];

	for (int i = 0; i < messageBytes.Length; i++)
	{
		byte keyByte = 0;

		for (int bit = 0; bit < 8; bit++)
		{
			uint newBit = Polynomial(lfsr);
			keyByte |= (byte)(newBit << bit);
			shiftedBits.Append(lfsr[0]);
			Array.Copy(lfsr, 1, lfsr, 0, lfsr.Length - 1);
			lfsr[^1] = newBit == 1 ? '1' : '0';
		}

		// XOR по байту
		crypted[i] = (byte)(messageBytes[i] ^ keyByte);
	}
	return (crypted, shiftedBits.ToString());
}

string lfsr = new string('1', 27);
Console.Write("Введите сообщение: ");
string message = Console.ReadLine();

byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);

(byte[] encrypted, string shiftedBits) = ciphre(lfsr.ToCharArray(), messageBytes);

(byte[] decrypted, _) = ciphre(lfsr.ToCharArray(), encrypted);

// Вывод результата
Console.WriteLine("Зашифрованное сообщение (в байтах): " + BitConverter.ToString(encrypted));
Console.WriteLine("Сохраненные выталкиваемые биты: " + shiftedBits);
Console.WriteLine("Расшифрованное сообщение: " + System.Text.Encoding.UTF8.GetString(decrypted));
