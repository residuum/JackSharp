// Author:
//       Thomas Mayer <thomas@residuum.org>
//
// Copyright (c) 2016 Thomas Mayer
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

namespace JackSharp.ApiWrapper
{
	[Flags]
	enum JackStatus
	{

		/// <summary>
		/// Overall operation failed.
		/// </summary>
		JackFailure = 0x01,

		/// <summary>
		/// The operation contained an invalid or unsupported option.
		/// </summary>
		JackInvalidOption = 0x02,

		/// <summary>
		/// The desired client name was not unique.  With the @ref
		/// JackUseExactName option this situation is fatal.  Otherwise,
		/// the name was modified by appending a dash and a two-digit
		/// number in the range "-01" to "-99".  The
		/// jack_get_client_name() function will return the exact string
		/// that was used.  If the specified @a client_name plus these
		/// extra characters would be too long, the open fails instead.
		/// </summary>
		JackNameNotUnique = 0x04,

		/// <summary>
		/// The JACK server was started as a result of this operation.
		/// Otherwise, it was running already.  In either case the caller
		/// is now connected to jackd, so there is no race condition.
		/// When the server shuts down, the client will find out.
		/// </summary>
		JackServerStarted = 0x08,

		/// <summary>
		/// Unable to connect to the JACK server.
		/// </summary>
		JackServerFailed = 0x10,

		/// <summary>
		/// Communication error with the JACK server.
		/// </summary>
		JackServerError = 0x20,

		/// <summary>
		/// Requested client does not exist.
		/// </summary>
		JackNoSuchClient = 0x40,

		/// <summary>
		/// Unable to load internal client
		/// </summary>
		JackLoadFailure = 0x80,

		/// <summary>
		/// Unable to initialize client
		/// </summary>
		JackInitFailure = 0x100,

		/// <summary>
		/// Unable to access shared memory
		/// </summary>
		JackShmFailure = 0x200,

		/// <summary>
		/// Client's protocol version does not match
		/// </summary>
		JackVersionError = 0x400,

		/// <summary>
		/// Backend error
		/// </summary>
		JackBackendError = 0x800,

		/// <summary>
		/// Client zombified failure
		/// </summary>
		JackClientZombie = 0x1000
	}
}
