using System.Runtime.InteropServices;
using System.Text;

namespace ClickHouse.Connector.Native.Structs;

[StructLayout(LayoutKind.Sequential)]
internal struct NativeStringView
{
    internal nint Data;
    internal nint Length;

    /*
     Possible conversion loss on 64-bit platforms since the Length field actually is of type size_t
     (which is 64-bit on 64-bit platforms).
     However, maximum length of string in c# is int.MaxValue, so longer strings are not supported anyways in c#.
     This should be safe since you can't insert a longer string when using this library (limited by c# strings).
     In the rare case that a different client inserts a longer string, the length will be truncated to int.MaxValue.
     But who will really have a >2GB string in a single field?
    */
    public override unsafe string ToString()
    {
        return new string((sbyte*)Data, 0, (int)Length, Encoding.UTF8);
    }
}