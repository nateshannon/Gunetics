using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunetics
{
    class GNA
    {
        public List<Gene> Genes { get; set; }
        public List<Polygene> Polygenes { get; set; }

        public GNA()
        {
            Genes = new List<Gene>();
            Polygenes = new List<Polygene>();
        }

        private Gene MakeGene(string Name, int Id, BitArray genomeBits, int bitIndex, int numberOfBits)
        {
            var newGene = new Gene();
            newGene.Name = Name;
            newGene.Id = Id;

            var newGeneValues = new bool[numberOfBits];
            for (var i = 0; i < numberOfBits; i++)
            {
                newGeneValues[i] = genomeBits[bitIndex + i];
            }

            newGene.Sequence = new BitArray(newGeneValues);

            return newGene;
        }

        public void SequenceGenes(string genome)
        {
            byte[] genomeBytes = Convert.FromBase64String(genome);
            BitArray genomeBits = new BitArray(genomeBytes);
            Genes = new List<Gene>();
            Polygenes = new List<Polygene>();

            Genes.Add(new Gene
            {
                Id = 0,
                Name = "Gun Length Class",
                Sequence = new BitArray(new bool[2] { genomeBits[0], genomeBits[1] })
            });
            Genes.Add(new Gene
            {
                Id = 1,
                Name = "Gun Length Subclass",
                Sequence = new BitArray(new bool[3] { genomeBits[2], genomeBits[3], genomeBits[4] })
            });
            Genes.Add(new Gene
            {
                Id = 2,
                Name = "Gun Length Minor Adjustment",
                Sequence = new BitArray(new bool[3] { genomeBits[5], genomeBits[6], genomeBits[7] })
            });
            
            Genes.Add(new Gene
            {
                Id = 3,
                Name = "Bore Type 1",
                Sequence = new BitArray(new bool[1] { genomeBits[8] })
            });
            Genes.Add(new Gene
            {
                Id = 4,
                Name = "Bore Type 2",
                Sequence = new BitArray(new bool[1] { genomeBits[9] })
            });
            Genes.Add(new Gene
            {
                Id = 5,
                Name = "Rifle Twist 01",
                Sequence = new BitArray(new bool[4] { genomeBits[10], genomeBits[11], genomeBits[12], genomeBits[13] })
            });
            Genes.Add(new Gene
            {
                Id = 6,
                Name = "Rifle Twist 02",
                Sequence = new BitArray(new bool[3] { genomeBits[14], genomeBits[15], genomeBits[16] })
            });
            Genes.Add(new Gene
            {
                Id = 7,
                Name = "Rifle Twist 03",
                Sequence = new BitArray(new bool[4] { genomeBits[17], genomeBits[18], genomeBits[19], genomeBits[20] })
            });

            Genes.Add(new Gene
            {
                Id = 8,
                Name = "Caliber 1",
                Sequence = new BitArray(new bool[1] { genomeBits[21] })
            });
            Genes.Add(new Gene
            {
                Id = 9,
                Name = "Caliber 2",
                Sequence = new BitArray(new bool[1] { genomeBits[22] })
            });
            Genes.Add(new Gene
            {
                Id = 10,
                Name = "Caliber 3",
                Sequence = new BitArray(new bool[1] { genomeBits[23] })
            });
            Genes.Add(new Gene
            {
                Id = 11,
                Name = "Caliber 4",
                Sequence = new BitArray(new bool[1] { genomeBits[24] })
            });

            // 25
            Genes.Add(MakeGene("Primary Color Hue", 12, genomeBits, 25, 9));
            Genes.Add(MakeGene("Primary Color Saturation", 13, genomeBits, 34, 7));
            Genes.Add(MakeGene("Primary Color Lightness", 14, genomeBits, 41, 7));


            







            var genes = new List<Gene>();
            genes.Add(Genes.Where(x => x.Id == 0).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 1).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 2).FirstOrDefault());
            Polygenes.Add(new Polygene { Name = "Gun Length", Decoder = "GetGunLength", Genes = genes });

            genes = new List<Gene>();
            genes.Add(Genes.Where(x => x.Id == 3).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 4).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 5).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 6).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 7).FirstOrDefault());
            Polygenes.Add(new Polygene { Name = "Gun Rifling", Decoder = "GetGunRifling", Genes = genes });

            genes = new List<Gene>();
            genes.Add(Genes.Where(x => x.Id == 8).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 9).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 10).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 11).FirstOrDefault());
            Polygenes.Add(new Polygene { Name = "Gun Caliber", Decoder = "GetGunCaliber", Genes = genes });

            genes = new List<Gene>();
            genes.Add(Genes.Where(x => x.Id == 12).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 13).FirstOrDefault());
            genes.Add(Genes.Where(x => x.Id == 14).FirstOrDefault());
            Polygenes.Add(new Polygene { Name = "Primary Color", Decoder = "GetPrimaryColor", Genes = genes });


            //return (genomeBits.Length).ToString();
        }

        public string GetSequenceCode(BitArray seq)
        {
            byte[] genomeBytes = new byte[(seq.Length - 1) / 8 + 1];
            seq.CopyTo(genomeBytes, 0);
            return Convert.ToBase64String(genomeBytes);
        }

        public string GetSequenceCode()
        {
            var genomeBools = new List<bool>();
            foreach (var gene in Genes)
            {
                foreach (var bit in gene.Sequence)
                {
                    genomeBools.Add(Convert.ToBoolean(bit));
                }
            }
            var gBools = new bool[genomeBools.Count];
            genomeBools.CopyTo(gBools);
            var gBitArray = new BitArray(gBools);

            return GetSequenceCode(gBitArray);
        }
    }
}
