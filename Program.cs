using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

var sha256 = SHA256.Create();
var stream = new MemoryStream();

static string AsString(byte[] hash)
{
  var builder = new StringBuilder();

  for (int i = 0; i < hash.Length; i++)
  {
    builder.Append(hash[i].ToString("x2"));
  }

  return builder.ToString().ToUpper();
}

static byte[] ToByteArray(string str)
{
  var bytes = new List<byte>();
  for (int i = 0; i < str.Length; i += 2)
  {
    var a = Convert.ToInt64(str.Substring(i, 2), 16);
    var b = Convert.ToChar(a);
    bytes.Add(Convert.ToByte(b));
  }

  return bytes.ToArray();
}

var to = "00B3D4EC5BF02E3D90DA8E6859CBBEB3BDD5CC54AAEEB3BAF1";
var fee = 1000;
var amount = 50;
var timestamp = "1644789410";

stream.Write(ToByteArray(to));
stream.Write(BitConverter.GetBytes(fee));
stream.Write(BitConverter.GetBytes(amount));
stream.Write(BitConverter.GetBytes(ulong.Parse(timestamp)));

Console.WriteLine(
  string.Join(",", BitConverter.GetBytes(ulong.Parse(timestamp)))
);

stream.Flush();
stream.Position = 0;

Console.WriteLine(
  AsString(sha256.ComputeHash(stream))
);