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

namespace JackSharp.Internal
{
	[Flags]
	enum JackOptions
	{

		/**
         * Null value to use when no option bits are needed.
         */
		JackNullOption = 0x00,

		/**
         * Do not automatically start the JACK server when it is not
         * already running.  This option is always selected if
         * \$JACK_NO_START_SERVER is defined in the calling process
         * environment.
         */
		JackNoStartServer = 0x01,

		/**
         * Use the exact client name requested.  Otherwise, JACK
         * automatically generates a unique one, if needed.
         */
		JackUseExactName = 0x02,

		/**
         * Open with optional <em>(char *) server_name</em> parameter.
         */
		JackServerName = 0x04,

		/**
         * Load internal client from optional <em>(char *)
         * load_name</em>.  Otherwise use the @a client_name.
         */
		JackLoadName = 0x08,

		/**
         * Pass optional <em>(char *) load_init</em> string to the
         * jack_initialize() entry point of an internal client.
         */
		JackLoadInit = 0x10,

		/**
         * pass a SessionID Token this allows the sessionmanager to identify the client again.
         */
		JackSessionID = 0x20}

	;
}