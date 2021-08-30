using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunetics
{
    static class Phenotype
    {

        public static string Decode(Polygene input)
        {

            switch (input.Decoder)
            {
                case "GetGunLength": { return GetGunLength(input).ToString(); }
                case "GetGunRifling": { return GetGunRifling(input).ToString(); }
                case "GetGunCaliber": { return GetGunCaliber(input).ToString(); }
                case "GetPrimaryColor": { return GetPrimaryColor(input).ToString(); }
                default: { return ""; }
            }


        }


        private static string GetPrimaryColor(Polygene input)
        {
            var hueBitArray = input.Genes.Where(x => x.Name == "Primary Color Hue").FirstOrDefault().Sequence;
            int[] hueArray = new int[1];
            hueBitArray.CopyTo(hueArray, 0);
            var hueInt = hueArray[0];
            double huePct = (360d / 512d) * hueInt;
            hueInt = Convert.ToInt32(huePct);

            var saturationBitArray = input.Genes.Where(x => x.Name == "Primary Color Saturation").FirstOrDefault().Sequence;
            int[] saturationArray = new int[1];
            saturationBitArray.CopyTo(saturationArray, 0);
            var saturationInt = saturationArray[0];
            double saturationPct = (100d / 128d) * saturationInt;
            saturationInt = Convert.ToInt32(saturationPct);

            var lightnessBitArray = input.Genes.Where(x => x.Name == "Primary Color Lightness").FirstOrDefault().Sequence;
            int[] lightnessArray = new int[1];
            lightnessBitArray.CopyTo(lightnessArray, 0);
            var lightnessInt = lightnessArray[0];
            double lightnessPct = (100d / 128d) * lightnessInt;
            lightnessInt = Convert.ToInt32(lightnessPct);

            return "H:" + hueInt.ToString() + ", S:" + saturationInt.ToString() + "%, L:" + lightnessInt.ToString() + "%";
        }

        private static double GetGunCaliber(Polygene input)
        {
            var caliberBitArray = new BitArray(4, false);
            caliberBitArray[0] = input.Genes.Where(x => x.Name == "Caliber 1").FirstOrDefault().Sequence[0];
            caliberBitArray[1] = input.Genes.Where(x => x.Name == "Caliber 2").FirstOrDefault().Sequence[0];
            caliberBitArray[2] = input.Genes.Where(x => x.Name == "Caliber 3").FirstOrDefault().Sequence[0];
            caliberBitArray[3] = input.Genes.Where(x => x.Name == "Caliber 4").FirstOrDefault().Sequence[0];

            int[] caliberArray = new int[1];
            caliberBitArray.CopyTo(caliberArray, 0);

            return Convert.ToDouble(caliberArray[0]);
        }

        private static double GetGunLength(Polygene input)
        {
            var overallLength = 0d;

            var classBitArray = input.Genes.Where(x => x.Name == "Gun Length Class").FirstOrDefault().Sequence;
            int[] classArray = new int[1];
            classBitArray.CopyTo(classArray, 0);
            overallLength += Convert.ToDouble(classArray[0]);

            var subclassBitArray = input.Genes.Where(x => x.Name == "Gun Length Subclass").FirstOrDefault().Sequence;
            int[] subclassArray = new int[1];
            subclassBitArray.CopyTo(subclassArray, 0);
            double subclassValue = subclassArray[0];
            subclassValue = ((subclassValue / 10) * 1.25);
            overallLength += subclassValue;

            var minorAdjBitArray = input.Genes.Where(x => x.Name == "Gun Length Minor Adjustment").FirstOrDefault().Sequence;
            int[] minorAdjArray = new int[1];
            minorAdjBitArray.CopyTo(minorAdjArray, 0);
            double minorAdjValue = minorAdjArray[0];
            minorAdjValue = (minorAdjValue - 3) / 16;
            overallLength += minorAdjValue;

            if (overallLength < 0) { overallLength = 0; }

            return overallLength;
        }

        private static string GetGunRifling(Polygene input)
        {
            var rifleTwist = "";

            var twistBitArray1 = input.Genes.Where(x => x.Name == "Rifle Twist 01").FirstOrDefault().Sequence;
            var twistBitArray2 = input.Genes.Where(x => x.Name == "Rifle Twist 02").FirstOrDefault().Sequence;
            var twistBitArray3 = input.Genes.Where(x => x.Name == "Rifle Twist 03").FirstOrDefault().Sequence;
            var boreTypeBitArray1 = input.Genes.Where(x => x.Name == "Bore Type 1").FirstOrDefault().Sequence;
            var boreTypeBitArray2 = input.Genes.Where(x => x.Name == "Bore Type 2").FirstOrDefault().Sequence;

            int[] twistArray1 = new int[1];
            int[] twistArray2 = new int[1];
            int[] twistArray3 = new int[1];
            int[] boreTypeArray1 = new int[1];
            int[] boreTypeArray2 = new int[1];

            twistBitArray1.CopyTo(twistArray1, 0);
            twistBitArray2.CopyTo(twistArray2, 0);
            twistBitArray3.CopyTo(twistArray3, 0);
            boreTypeBitArray1.CopyTo(boreTypeArray1, 0);
            boreTypeBitArray2.CopyTo(boreTypeArray2, 0);

            if ((boreTypeArray1[0] == 0 && boreTypeArray2[0] == 0) || (boreTypeArray1[0] == 1 && boreTypeArray2[0] == 1))
            {
                rifleTwist = "Smooth Bore";
            }
            else
            {
                var twist = 0;
                twist = twistArray1[0] + twistArray2[0];
                if (twist < 1) { twist = 1; }
                var twistMult = 0d;
                twistMult = (Convert.ToDouble(twistArray3[0]) / 10) + 1;
                twistMult = (twistMult * twist);
                twist = Convert.ToInt32(twistMult);
                if (twist < 1) { twist = 1; }
                rifleTwist = "Rifle Ratio 1:" + twist;
            }

            return rifleTwist;
        }


    }
}
