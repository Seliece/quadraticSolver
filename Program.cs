using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


namespace CommandLineCalculator
{
    public enum EquationType
            {
                normal,
                top,
                intersect
            }
    public class Program {
        public static void Main(String[] args) {
            if (args.Length != 1) {
                Console.WriteLine("write only 1 input");
                return;
            }

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine(args[i]);
            }

            string input = args[0];
            EquationType equationType = Parser.Detector(input);
            Parser.Parse(input, equationType);
        }   
    }
    public static class Parser {
        //1. ax^2+bx+c
        //2. (x-p)(x-q)
        //3. (x-g)^2+p
        public static EquationType Detector(string input) {
            string Type1Path = @"[0-9]*x\^2(\+|-)[0-9]*x(\+|-)[0-9]*";
            Regex Type1 = new Regex(Type1Path);

            string Type2Path = @"\(x(\+|-)[0-9]*\)\(x(\+|-)[0-9]*\)";
            Regex Type2 = new Regex(Type2Path);

            string Type3Path = @"\(x(-|\+)[0-9]*\)\^2(-|\+)[0-9]*";
            Regex Type3 = new Regex(Type3Path);

            if (Type1.Match(input).Success) {
                Console.WriteLine("type1");
                return EquationType.normal;
            } else if (Type2.Match(input).Success){
                Console.WriteLine("type2");
                return EquationType.intersect;
            } else if (Type3.Match(input).Success) {
                Console.WriteLine("type3");
                return EquationType.top;
            }
            
            Console.WriteLine("invalid input");
            return 0;
        }
        
        public static string Parse(string input, EquationType type) {
            
            switch (type)
            {
                case EquationType.normal:
                    NormalParse(input);
                    return string.Empty;

                case EquationType.intersect:
                    intersectParse(input);
                    return string.Empty;

                case EquationType.top:
                    topParse(input);
                    return string.Empty;

                default:
                    return string.Empty;
            }
        }

        static string NormalParse(string input) {
            return string.Empty;
        }

        static string intersectParse(string input) {
            return string.Empty;
        }

        static string topParse(string input) {
            return string.Empty;
        }
    }
}

