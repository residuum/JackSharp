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

		/**
         * Overall operation failed.
         */
		JackFailure = 0x01,

		/**
         * The operation contained an invalid or unsupported option.
         */
		JackInvalidOption = 0x02,

		/**
         * The desired client name was not unique.  With the @ref
         * JackUseExactName option this situation is fatal.  Otherwise,
         * the name was modified by appending a dash and a two-digit
         * number in the range "-01" to "-99".  The
         * jack_get_client_name() function will return the exact string
         * that was used.  If the specified @a client_name plus these
         * extra characters would be too long, the open fails instead.
         */
		JackNameNotUnique = 0x04,

		/**
         * The JACK server was started as a result of this operation.
         * Otherwise, it was running already.  In either case the caller
         * is now connected to jackd, so there is no race condition.
         * When the server shuts down, the client will find out.
         */
		JackServerStarted = 0x08,

		/**
         * Unable to connect to the JACK server.
         */
		JackServerFailed = 0x10,

		/**
         * Communication error with the JACK server.
         */
		JackServerError = 0x20,

		/**
         * Requested client does not exist.
         */
		JackNoSuchClient = 0x40,

		/**
         * Unable to load internal client
         */
		JackLoadFailure = 0x80,

		/**
         * Unable to initialize client
         */
		JackInitFailure = 0x100,

		/**
         * Unable to access shared memory
         */
		JackShmFailure = 0x200,

		/**
         * Client's protocol version does not match
         */
		JackVersionError = 0x400,

		/**
         * Backend error
         */
		JackBackendError = 0x800,

		/**
         * Client zombified failure
         */
		JackClientZombie = 0x1000
	}
}