// Merle Roji

using System.Text;

namespace T02
{
    /// <summary>
    /// Quality of life functions; strings related.
    /// </summary>
    public static class StringQoL
    {
        /// <summary>
        /// Returns the longest common substring and its length.
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <param name="subStr"></param>
        /// <returns></returns>
        public static int LongestCommonSubstring(string str1, string str2, out string subStr)
        {
            subStr = string.Empty;

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return 0;

            int[,] num = new int[str1.Length, str2.Length];
            int maxlen = 0;
            int lastSubsBegin = 0;
            StringBuilder subStrBuilder = new StringBuilder();

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                    {
                        num[i, j] = 0;
                    }
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];

                        if (num[i, j] > maxlen)
                        {
                            maxlen = num[i, j];

                            int thisSubsBegin = i - num[i, j] + 1;

                            if (lastSubsBegin == thisSubsBegin)
                            {
                                subStrBuilder.Append(str1[i]);
                            }
                            else
                            {
                                lastSubsBegin = thisSubsBegin;
                                subStrBuilder.Length = 0;
                                subStrBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                            }
                        }
                    }
                }
            }

            subStr = subStrBuilder.ToString();

            return maxlen;
        }
    }
}