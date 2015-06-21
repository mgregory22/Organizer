using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace MSG.Types.String
{
    /// <summary>
    ///   String formatting procedures
    /// </summary>
    public class Format
    {
        public static string ToLiteral(string input)
        {
            /* damn thing is too smart
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
            */
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\b':
                        sb.Append(@"\b");
                        break;
                    case '\t':
                        sb.Append(@"\t");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        public static string Padding(int length)
        {
            return new string(' ', length);
        }
    }
}
