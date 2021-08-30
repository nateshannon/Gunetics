using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gunetics
{
    static class Reproduction
    {
        public static GNA Breed(List<GNA> parents)
        {
            var child = new GNA();
            var childGenes = new List<Gene>();
            var genePool = new List<Gene>();
            var geneCount = parents[0].Genes.Count();
            var parentCount = parents.Count();

            foreach (var parent in parents)
            {
                genePool.AddRange(parent.Genes);
            }

            Random rnd = new Random();

            for (var i = 0; i < geneCount; i++)
            {
                int rInt = rnd.Next(0, parentCount);
                var theseGenes = genePool.Where(x => x.Id == i).ToList();
                childGenes.Add(theseGenes[rInt]);
            }
            child.Genes = childGenes;

            return child;
        }
    }
}
