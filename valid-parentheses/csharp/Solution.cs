public class Solution
{
    // Given a string containing only the characters ( ) [ ] { }, return true
    // if every bracket is closed by the same type of bracket, in the correct
    // order, and every closing bracket has an opening bracket to match.
    public bool IsValid(string s)
    {
        Stack<char> stack = new Stack<char>();
        char item;
        
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            switch (c)
            {
                case '(':
                case '[':
                case '{':
                    stack.Push(c);
                    break;
                case ')':
                    if (stack.Count > 0) {
                        item = stack.Pop();
                    } else
                    {
                        return false;
                    }
                    if (item == '(') {
                        break;
                    } else {
                        return false;
                    }
                case ']':
                    if (stack.Count > 0) {
                        item = stack.Pop();
                    } else
                    {
                        return false;
                    }
                    if (item == '[') {
                        break;
                    } else {
                        return false;
                    }
                case '}':
                    if (stack.Count > 0) {
                        item = stack.Pop();
                    }  else
                    {
                        return false;
                    }
                    if (item == '{') {
                        break;
                    } else {
                        return false;
                    }
            }
        }

        if (stack.Count == 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}