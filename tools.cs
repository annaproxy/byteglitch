using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace byteglitch {

    public class Glitcher {

        public Glitcher(){
            
        }
        public int Glitch(
                string filename, 
                string output,
                byte[] find,
                byte[] replace) {
            /*
            Replace all occurrences of 'find' with 'replace' 
            Writes to filename output.
            Args:
                filename : Jpeg filename
                output: Output filename
                find, replace: byte arrays of length 2,3
            Output:
                Amount of byte[] replaced
            */

            if (find.Length != replace.Length) {
                throw new InvalidOperationException("For jpeg glitch, please ensure length is the same");
            }
            int result = 0;
            int index;
            byte[] bytes = File.ReadAllBytes(filename);

            // Loop over all bytes in the file
            for (int i = 0; i < bytes.Length; i++) {
                bool gelijk = true;
                index = 0;
                // Find a substring(sub byte[], rather) that is the same
                for (int j = i; j < i + find.Length; j++) {
                    if (j >= bytes.Length) {
                        gelijk = false;
                        break;
                    }
                    if (bytes[j] != find[index]) {
                        gelijk = false;
                        break;
                    }
                    index++;
                }
                if (gelijk) {
                    result++;
                    index = 0;
                    for (int j = i; j < i + find.Length; j++) {
                        bytes[j] = replace[index];
                        index++;
                    }
                }
            }
            File.WriteAllBytes(output, bytes);
            return result;
        }
    }
    public static class Tools {
        public static string BytestoHexString(byte[] bytes) {
            string t = string.Empty;
            for (int i = 0; i < bytes.Length; i++) {
                byte b = bytes[i];
                string d = b.ToString("X2");
                t += d;
            }
            return t;
        }
        public static byte[] HexStringtoBytes(string s) {
            /*
            Args:
                s : input string that represents hexadecimal numbers, ie:
                "F044" of length 2.
                Altnernatively, you can provide "XX" to randomly generate parts of the output
            Output:
                Byte array representing the hex string.
            */
            if (s.Length % 2 != 0) {
                throw new InvalidOperationException("Please provide an even number length string");
            }
            Random r = new Random();
            List<byte> result = new List<byte>();
            byte nummer;
            
            for (int i = 0; i < s.Length; i = i + 2) {
                string sub = s.Substring(i, 2);
                if (sub == "XX") {
                    nummer = (byte)r.Next(255);
                }
                
                else if (!byte.TryParse(sub, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out nummer)) {
                    throw new InvalidOperationException("Not a hexadecimal string" + sub);
                }
                result.Add(nummer);
            }
            return result.ToArray();
        }
        public static string GetOutFilename(string path, string s) {
            string ext = Path.GetExtension(s);
            s = Path.GetFileNameWithoutExtension(s);
            s = s + "_glitched" + ext;
            s = path + s;
            return s;
        }
    }
}