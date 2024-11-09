namespace FileWorker.Utility
{
    public static class RandomNumGen
    {
        private static readonly Random _seedGen = new();

        //ThreadStatic since Random is not thread-safe
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