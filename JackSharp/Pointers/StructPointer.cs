// Author:
//	   jjxtra
//
// Copyright (c) 2016 jjxtra <http://stackoverflow.com/a/35415879/124983>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Runtime.InteropServices;

namespace JackSharp.Pointers
{
	class StructPointer<T> where T:struct
	{
		/// <summary>
		/// Pointer
		/// </summary>
		readonly IntPtr _pointer;

		/// <summary>
		/// Number of elements in pointer
		/// </summary>
		public int Size { get; private set; }

		/// <summary>
		/// Array
		/// </summary>
		public T[] Array { get; set; }

		/// <summary>
		/// Copy Array to pointer. Must be called after operating on Array and before using the pointer in P/Invoke.
		/// </summary>
		public void CopyToPointer ()
		{
			if (_pointer == IntPtr.Zero) {
				return;
			}
			int length = Math.Min (Size, Array.Length);
			int byteCount = length * Marshal.SizeOf (typeof(T));
			GCHandle handle = GCHandle.Alloc (Array, GCHandleType.Pinned);
			unsafe {
				for (int i = 0; i < byteCount; i++) {
					*(((byte*)_pointer) + i) = *(((byte*)handle.AddrOfPinnedObject ()) + i);
				}
			}
			handle.Free ();

			if (Size > Array.Length) {
				unsafe {
					int byteCount2 = byteCount + (Marshal.SizeOf (typeof(T)) * (Size - Array.Length));
					for (int i = byteCount; i < byteCount2; i++) {
						*(((byte*)_pointer) + i) = 0;
					}
				}
			}
		}

		/// <summary>
		/// Copy Pointer to Array
		/// </summary>
		public T[] CopyFromPointer ()
		{
			if (_pointer == IntPtr.Zero) {
				return new T[Size];
			}
			T[] array = new T[Size];
			GCHandle handle = GCHandle.Alloc (array, GCHandleType.Pinned);
			int byteCount = array.Length * Marshal.SizeOf (typeof(T));
			unsafe {
				for (int i = 0; i < byteCount; i++) {
					*(((byte*)handle.AddrOfPinnedObject ()) + i) = *(((byte*)_pointer) + i);
				}
			}
			handle.Free ();
			return array;
		}

		/// <summary>
		/// Constructor. Copies pointer to Array.
		/// </summary>
		/// <param name="pointer">Pointer</param>
		/// <param name="length">Number of elements in pointer</param>
		public StructPointer (IntPtr pointer, uint length)
		{
			_pointer = pointer;
			Size = (int)length; // number of elements in pointer, not number of bytes
			Array = CopyFromPointer ();
		}
	}
}
