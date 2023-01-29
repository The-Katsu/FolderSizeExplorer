using System;

namespace FolderSizeExplorer.Services.Helpers
{
    internal static class SizeUnitConverter
    {
        public static int Convert(string str)
        {
            switch (str)
            {
                case "B":
                    return 0;
                case "KB":
                    return 1;
                case "MB":
                    return 2;
                case "GB": 
                    return 3;
                case "TB":
                    return 4;
            }

            return 0;
        }
        
        public static string ToHumanReadSize(long size, string unit)
        {
            var numericValue = SizeUnitConverter.Convert(unit);
            int del;
            if (string.Equals("B", unit)) del = 1;
            else del = (int) Math.Pow(1024, numericValue);

            var unitSize = size / del;
            if (unitSize < 0) unitSize = 0;

            return $"{unitSize} {unit}";
        }
    }
}