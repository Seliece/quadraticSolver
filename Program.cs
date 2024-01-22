using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


namespace CommandLineCalculator
{
    public enum EquationType
            {
                normal,
                top,
                intersect,
                invalid
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
            return EquationType.invalid;
        }
        
        public static string Parse(string input, EquationType type) {
            
            switch (type)
            {
                case EquationType.normal:
                    NormalParse(input);
                    return string.Empty;

                case EquationType.intersect:
                    IntersectParse(input);
                    return string.Empty;

                case EquationType.top:
                    TopParse(input);
                    return string.Empty;

                default:
                    return string.Empty;
            }
        }

        static int[] NormalParse(string input) {
            //ax^2+bx+c
            int[] nums = new int[3];
            int locationA,locationB,locationC;

            locationA = input.IndexOf("x^2");
            nums[0] = int.Parse(input.Substring(0, locationA));

            locationB = input.IndexOf("x", locationA + 3);
            nums[1] = int.Parse(input.Substring(locationA + 3, locationB - (locationA + 3)));

            locationC = input.IndexOf("+", locationB + 1); 
            if (locationC == -1) {

                locationC = input.IndexOf("-", locationB + 1);
            }
            nums[2] = int.Parse(input.Substring(locationB + 1));
            
            Console.WriteLine($"A={nums[0]},B={nums[1]},C={nums[2]}");
            return nums;
        }

        static int[] IntersectParse(string input) {
            //(x-p)(x-q)
            int[] nums = new int[2];
            int locationP;

            locationP = input.IndexOf(")(");
            nums[0] = int.Parse(input.Substring(2,locationP -2));
            
            nums[1] = int.Parse(input.Substring(locationP + 3, input.Length -2 - (locationP + 2)));
            
            Console.WriteLine($"P={nums[0]},Q={nums[1]}");
            return nums;
        }

        static int[] TopParse(string input) {
            //(x-g)^2+p
            int[] nums = new int[2];
            int locationG;

            locationG = input.IndexOf(")^2");
            nums[0] = int.Parse(input.Substring(2,locationG - 2));

            nums[1] = int.Parse(input.Substring(locationG + 3));

            Console.WriteLine($"G={nums[0]},P={nums[1]}");
            return nums;  
        }
    }
}

