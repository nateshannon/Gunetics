using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunetics
{
    class Program
    {
        static void Main(string[] args)
        {

            var gna1 = new GNA();
            var gna2 = new GNA();
            var gna3 = new GNA();
            gna1.SequenceGenes("QVRdIcBx");
            gna2.SequenceGenes("L0LXGaqM");
            gna3.SequenceGenes("Blb1kchU");

            var parentGNA = new List<GNA>();
            parentGNA.Add(gna1);
            parentGNA.Add(gna2);
            parentGNA.Add(gna3);

            var childGNA = Reproduction.Breed(parentGNA);
            var childSeq = childGNA.GetSequenceCode();
            childGNA.SequenceGenes(childSeq);


            //var gnaBits = new BitArray(new bool[48] { false, true, true, false, false, false, false, false,
            //                                          false, true, true, false, true, false, true, false,
            //                                          true, false, true, false, true, true, true, true,
            //                                          true, 
            //    // HSL Primary      
            //    false, false, false, true, false, false, true, false, false,
            //    false, true, false, false, true, true, false,
            //    false, true, false, true, false, true, false});
            //gna.SequenceGenes(gna.GetSequenceCode(gnaBits));


            // muzzle brake
            // secondary color HSL
            // barrel shroud
            // barrel shroud hole pattern (50% is optimal)
            // barrel shroud geometry
            // barrel shroud size
            // barrel shroud material
            // barrel material
            // color blend mode
            // barrel count
            // Ammo feed mechanism (belt, captive belt, single autoloader, magazine)
            // Receiver shape
            // 
            // Ammo pattern (10101111 00001010)
            // specialty ammo type (mutation accessible) - None, Stun/EMP, Blade-Magnet, Incendiary, Armor-Piercing, Explosive
            // specialty ammo potency
            // later: wing geometry point 1 and 2, root is one side of rectangle




            Console.WriteLine("");
            Console.WriteLine("=== GNA ===");

            

            Console.WriteLine("Child Sequence: " + childSeq); ;


            Console.WriteLine("");
            Console.WriteLine("=== DECODED ===");
            Console.WriteLine("Barrel Length: " + Phenotype.Decode(childGNA.Polygenes.Where(x => x.Name == "Gun Length").FirstOrDefault()));
            Console.WriteLine("Barrel Bore: " + Phenotype.Decode(childGNA.Polygenes.Where(x => x.Name == "Gun Rifling").FirstOrDefault()));
            Console.WriteLine("Barrel Caliber: " + Phenotype.Decode(childGNA.Polygenes.Where(x => x.Name == "Gun Caliber").FirstOrDefault()));
            Console.WriteLine("Primary Color: " + Phenotype.Decode(childGNA.Polygenes.Where(x => x.Name == "Primary Color").FirstOrDefault()));



            


            Console.Read();





        }



        public static void PrintValues(IEnumerable myList, int myWidth)
        {
            int i = myWidth;
            foreach (Object obj in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }
    }
}
