using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace byteglitch{
    public static class MainGlitch{

        public static void Main(string[] args){
            // Usage: glitch.exe filename [hex1] [hex2]
            string filename = args[0];
            string h1, h2;
            if (args.Length == 1){
                h1 = "XX"; h2 = "XX";
            }
            else{
                h1 = args[1]; 
                h2 = args[2];
            }
            byte[] find = Tools.HexStringtoBytes(h1);
            byte[] replace = Tools.HexStringtoBytes(h2);
            Console.WriteLine("Replacing" + Tools.BytestoHexString(find) + " " + Tools.BytestoHexString(replace));

            Glitcher g = new Glitcher();
            string outfile = Tools.GetOutFilename("./outputs/", filename);
            int replaced = g.Glitch(filename, outfile, find, replace );
            Console.WriteLine("Replaced " + replaced + " occurences. output written to " + outfile );
        }


    }


}