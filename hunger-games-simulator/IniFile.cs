using System;
using System.Text;
using System.Runtime.InteropServices;


namespace hunger_games_simulator
{
    // Set the IniFileName class within the INI Namespace.
    public class IniFile
    {
        //    Imports the Win32 Function "GetPrivateProfileString"
        ///   from the "Kernel32" class.
        //    I use 3 methods to gather the information. All have the same name
        //    as defind by the Win32 Function "GetPrivateProfileString"
        //
        //    First Method, Gathers the Value, as the SectionHeader and EntryKey are know.
        //
        //    Second Method, Gathers a list of EntryKey for the known SectionHeader 
        //
        //    Third Method, Gathers a list of SectionHeaders.


        // First Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key,
               string Value, StringBuilder Result, int Size, string FileName);

        // Second Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, int Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);

        // Third Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int Section, string Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);

        // Set the IniFileName passed from the Main Application.
        public string path;
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        // The Function called to obtain the SectionHeaders,
        // and returns them in an Dynamic Array.
        public string[] GetSectionNames()
        {
            //    Sets the maxsize buffer to 500, if the more
            //    is required then doubles the size each time.
            for (int maxsize = 500; true; maxsize *= 2)
            {
                //    Obtains the information in bytes and stores
                //    them in the maxsize buffer (Bytes array)
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(0, "", "", bytes, maxsize, path);

                // Check the information obtained is not bigger
                // than the allocated maxsize buffer - 2 bytes.
                // if it is, then skip over the next section
                // so that the maxsize buffer can be doubled.
                if (size < maxsize - 2)
                {
                    // Converts the bytes value into an ASCII char. This is one long string.
                    string Selected = Encoding.ASCII.GetString(bytes, 0,
                                               size - (size > 0 ? 1 : 0));
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array
                    return Selected.Split(new char[] { '\0' });
                }
            }
        }
        // The Function called to obtain the EntryKey's from the given
        // SectionHeader string passed and returns them in an Dynamic Array
        public string[] GetEntryNames(string section)
        {
            //    Sets the maxsize buffer to 500, if the more
            //    is required then doubles the size each time. 
            for (int maxsize = 500; true; maxsize *= 2)
            {
                //    Obtains the EntryKey information in bytes
                //    and stores them in the maxsize buffer (Bytes array).
                //    Note that the SectionHeader value has been passed.
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(section, 0, "", bytes, maxsize, path);

                // Check the information obtained is not bigger
                // than the allocated maxsize buffer - 2 bytes.
                // if it is, then skip over the next section
                // so that the maxsize buffer can be doubled.
                if (size < maxsize - 2)
                {
                    // Converts the bytes value into an ASCII char.
                    // This is one long string.
                    string entries = Encoding.ASCII.GetString(bytes, 0,
                                              size - (size > 0 ? 1 : 0));
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array
                    return entries.Split(new char[] { '\0' });
                }
            }
        }

        // The Function called to obtain the EntryKey Value from
        // the given SectionHeader and EntryKey string passed, then returned
        public object GetEntryValue(string section, string entry)
        {
            //    Sets the maxsize buffer to 250, if the more
            //    is required then doubles the size each time. 
            for (int maxsize = 250; true; maxsize *= 2)
            {
                //    Obtains the EntryValue information and uses the StringBuilder
                //    Function to and stores them in the maxsize buffers (result).
                //    Note that the SectionHeader and EntryKey values has been passed.
                StringBuilder result = new StringBuilder(maxsize);
                int size = GetPrivateProfileString(section, entry, "",
                                                   result, maxsize, path);
                if (size < maxsize - 1)
                {
                    // Returns the value gathered from the EntryKey
                    return result.ToString();
                }
            }
        }
    }
}