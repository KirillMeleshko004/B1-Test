namespace FileWorker.Utility
{
    public static class RandomGen
    {
        private static readonly Random _seedGen = new();

        [ThreadStatic]
        private static Random? _rand;

        public static int Next(int max)
        {
            if (_rand == null)
            {
                int seed;
                lock (_seedGen)
                {
                    seed = _seedGen.Next();
                }
                _rand = new Random(seed);
            }

            return _rand.Next(max);
        }

        public static int Next(int min, int max)
        {
            if (_rand == null)
            {
                int seed;
                lock (_seedGen)
                {
                    seed = _seedGen.Next();
                }
                _rand = new Random(seed);
            }

            return _rand.Next(min, max);
        }

        public static double NextDouble()
        {
            if (_rand == null)
            {
                int seed;
                lock (_seedGen)
                {
                    seed = _seedGen.Next();
                }
                _rand = new Random(seed);
            }

            return _rand.NextDouble();
        }
    }
}