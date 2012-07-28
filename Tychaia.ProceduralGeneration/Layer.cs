using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tychaia.ProceduralGeneration
{
    public abstract class Layer
    {
        [NonSerialized]
        private int m_Seed;

        /// <summary>
        /// The world seed.
        /// </summary>
        protected int Seed
        {
            get
            {
                return this.m_Seed;
            }
        }
        
        [Obsolete("This constructor is only for XML serialization / deserialization.", true)]
        public Layer()
        {
        }

        protected Layer(int seed)
        {
            this.m_Seed = seed;
        }

        protected Random GetCellRNG(int x, int y)
        {
            /* From: http://stackoverflow.com/questions/2890040/implementing-gethashcode
             * Although we aren't implementing GetHashCode, it's still a good way to generate
             * a unique number given a limited set of fields */
            unchecked
            {
                long seed = x * 3661988493967 + y;
                seed += x * 2990430311017;
                seed *= y * 14475080218213;
                seed -= y * 28124722524383;
                seed *= x * 16099760261113;
                // Prevents the seed from being 0 along an axis.
                seed += (x - 199) * (y - 241) * 9018110272013;

                return new Random((int)seed);
            }
        }

        public abstract int[] GenerateData(int x, int y, int width, int height);
    }
}
