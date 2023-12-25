
namespace Advent23.Days.Day12
{
    public class ConditionRecord
    {
        public required string Text { get; init; }
        public required int[] Sizes { get; init; }

        public static ConditionRecord Parse(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.TrimEntries);
            string[] integerStrs = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            ConditionRecord record = new()
            {
                Text = parts[0],
                Sizes = integerStrs
                        .Select(int.Parse)
                        .ToArray()
            };

            return record;
        }

        public static bool Fits(string text, int start, int end)
        {
            // Out of bounds check
            if (start - 1 < 0 || end + 1 >= text.Length)
            {
                return false;
            }

            // Checkin if segment can be surrounded by non-#
            if (text[start - 1] == '#' || text[end + 1] == '#')
            {
                return false;
            }

            // Are we skipping any "#"
            if (text.Take(text.Length - start).Contains('#'))
            {
                return false;
            }

            for (int i = start; i <= end; i++)
            {
                if (text[i] == '.') return false;
            }

            return true;
        }

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (obj is not ConditionRecord other) return false;

            if (Text != other.Text) return false;

            if (!Enumerable.SequenceEqual(Sizes, other.Sizes)) return false;

            return true;
        }

        public static bool operator ==(ConditionRecord b1, ConditionRecord b2)
        {
            if ((object)b1 == null)
                return (object)b2 == null;

            return b1.Equals(b2);
        }

        public static bool operator !=(ConditionRecord b1, ConditionRecord b2)
        {
            return !(b1 == b2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Text.GetHashCode();
                hashCode = (hashCode * 397) ^ Sizes.GetHashCode();

                if (Sizes == null)
                {
                    return 0;
                }
                int arrayHash = 17;
                foreach (var element in Sizes)
                {
                    arrayHash = arrayHash * 31 + element.GetHashCode();
                }
                hashCode = (hashCode * 397) ^ arrayHash;

                return hashCode;
            }
        }
    }
}