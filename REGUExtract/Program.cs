namespace REGUExtract
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Verify args is not bad
            if (args == null)
                return;
            if (args.Length == 0)
                return;

            // Loop through and extract
            foreach (string arg in args)
            {
                // Check if arg is null, empty, not a file, or the name is not a match
                if (arg == null)
                    continue;
                if (arg == string.Empty)
                    continue;
                if (!File.Exists(arg))
                    continue;

                // Verify file name
                string name = Path.GetFileName(arg);
                if (name != "REGU.DAT" && name != "regulation.bnd")
                    continue;

                // Check if we are unpacking or repacking based on file name
                bool extract = name == "REGU.DAT";

                // Get output directory
                string outDir = Path.GetDirectoryName(arg);
                if (outDir == null)
                    continue;

                // Determine the output name and final path
                string outName = extract ? "regulation.bnd" : "REGU.DAT";
                string outPath = Path.Combine(outDir, outName);

                // Backup file if it already exists and there is no backup
                if (File.Exists(outPath))
                    if (!File.Exists($"{outPath}.bak"))
                        File.Move(outPath, $"{outPath}.bak");

                // Attempt unpack or repack
                try
                {
                    File.WriteAllBytes(outPath, extract ? Unpack(File.ReadAllBytes(arg)) : Repack(File.ReadAllBytes(arg)));
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.ReadLine();
#endif
                    continue;
                }
            }
        }

        /// <summary>
        /// Unpack a REGU.DAT file.
        /// </summary>
        /// <param name="bytes">The bytes of the REGU.DAT file.</param>
        /// <returns>The bytes of the BND3 Binder file within the REGU.DAT file.</returns>
        static byte[] Unpack(byte[] bytes)
        {
            // Convert the first 4 bytes into the length int
            int length = BitConverter.ToInt32(new byte[] { bytes[3], bytes[2], bytes[1], bytes[0] }, 0);

            // Create a list to manage removing unneeded data
            var list = new List<byte>();
            list.AddRange(bytes);

            // Remove the length bytes and the padding
            Console.WriteLine(bytes.Length - (length + 4));
            list.RemoveRange(length + 4, bytes.Length - (length + 4));
            list.RemoveRange(0, 4);

            // Return the bnd bytes
            return list.ToArray();
        }

        /// <summary>
        /// Repack a regulation.bnd file into a REGU.DAT file.
        /// </summary>
        /// <param name="bytes">The bytes of the regulation.bnd file.</param>
        /// <returns>The bytes of the REGU.DAT file.</returns>
        static byte[] Repack(byte[] bytes)
        {
            // Create list
            var list = new List<byte>();

            // Get length, the bytes of the length, then reverse it for big endian
            int length = bytes.Length;
            byte[] length_bytes = BitConverter.GetBytes(length);
            Array.Reverse(length_bytes);

            // Add the length bytes, add the bnd bytes, add the remaining padding
            list.AddRange(length_bytes);
            list.AddRange(bytes);
            list.AddRange(new byte[256000 - length]);

            // Return the REGU.DAT bytes
            return list.ToArray();
        }
    }
}