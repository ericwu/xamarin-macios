﻿//
// Test the generated API selectors against typos or non-existing cases
//
// Authors:
//	Paola Villarreal  <paola.villarreal@xamarin.com>
//
// Copyright 2015 Xamarin Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
#if XAMCORE_2_0
#if MONOMAC
using AppKit;
#else
using UIKit;
#endif
using Foundation;
#else
#if MONOMAC
using MonoMac.AppKit;
#else
using MonoTouch.UIKit;
#endif
using MonoTouch.Foundation;
#endif

namespace Introspection
{
	public abstract class ApiTypoTest : ApiBaseTest
	{
		protected ApiTypoTest ()
		{
			ContinueOnFailure = true;
		}

		public virtual bool Skip (Type baseType, string typo) {
			return SkipAllowed (baseType.Name, null, typo);
		}

		public virtual bool Skip (MemberInfo methodName, string typo) {
			return SkipAllowed (methodName.DeclaringType.Name, methodName.Name, typo);
		}

		HashSet<string> allowed = new HashSet<string> () {
			"Aac",
			"Accurracy",
			"Achivements",
			"Acos",
			"Actionname",
			"Activitiy",
			"Addr",
			"Adjustmentfor",
			"Aifc",
			"Aiff",
			"Amete",
			"Amr",
			"Anglet",
			"Arraycollation",
			"Asin",
			"Atan",
			"Attrib",
			"Attributevalue",
			"Audiofile",
			"Automounted",
			"Autoredirect",
			"Backface",
			"Bary",
			"Batc",
			"Bim",
			"Biquad",
			"Bitangent",
			"Blinn",
			"Blit",
			"Bokeh",
			"Bsln",
			"Bssid",
			"Bzip",
			"Cabac",
			"Caf", // acronym: Core Audio Format
			"Cancellable",
			"Cavlc",
			"Celp", // MPEG4ObjectID
			"Characterteristic",
			"Chromaticities",
			"Ciff",
			"Cinepak",
			"Clearcoat",
			"Colos",
			"Commerical",
			"Composable",
			"Conflictserror",
			"Connnect",
			"Counterclock",
			"Craete",
			"Cubemap",
			"Cmyk", // acronym: Cyan, magenta, yellow and key
			"Daap",
			"Dav",
			"Dcip", // acronym: Digital Cinema Implementation Partners
			"Deca",
			"Decomposables",
			"Deinterlace",
			"Descendents",
			"Descrete",
			"Differental",
			"Diffie",
			"Directionfor",
			"Dist",
			"dlclose",
			"dlerror",
			"Dlfcn",
			"dlopen",
			"dlsym",
			"Dng",
			"Dns",
			"Dont",
			"Dop",
			"Downsample",
			"Downmix", // Sound terminology that means making a stereo mix from a 5.1 surround mix.
			"Dpa",
			"Dpad", // Directional pad (D-pad)
			"Droste",
			"Dtls",
			"dy",
			"Ebu",
			"Ecc",
			"Eof", // acronym End-Of-File
			"Emagic",
			"Emaili",
			"Eppc",
			"Exhange",
			"Exp",
			"Flipside",
			"Formati",
			"Fov",
			"Framebuffer",
			"Framesetter",
			"Freq",
			"Ftps",
			"Func",
			"Gadu",
			"Geocoder",
			"Gpp",
			"Hdmi",
			"Hdr",
			"Hevc", // CMVideoCodecType / High Efficiency Video Coding
			"Hfp",
			"Hipass",
			"Hls",
			"Hrtf", // acronym used in AUSpatializationAlgorithm
			"Hvxc", // MPEG4ObjectID
			"Icq",
			"Identd",
			"Imagefor",
			"Imap",
			"Imaps",
			"Img",
			"Inser",
			"Interac",
			"Interframe",
			"Interitem",
			"Intermenstrual",
			"Intoi",
			"Ios",
			"Ipp",
			"Iptc",
			"Ircs",
			"Itu",
			"Jfif",
			"Json",
			"Keyerror",
			"Keyi",
			"Keyspace",
			"ks",
			"Langauges",
			"Ldaps",
			"Lerp",
			"Linecap",
			"Lingustic",
			"Lod",
			"Lopass",
			"Lowlevel",
			"Matchingcoalesce",
			"Metacharacters",
			"Minification",
			"Mobike", // acronym
			"Morpher",
			"Mtu", // acronym
			"Mtc", // acronym
			"Mul",
			"Mult",
			"Multipeer",
			"Muxed",
			"nfloat",
			"nint",
			"Nntps",
			"Ntlm",
			"Ntsc",
			"nuint",
			"Numbernumber",
			"Nyquist",
			"Objectfor",
			"Occlussion",
			"Ocurrences",
			"Oid",
			"Oneup", // TVElementKeyOneupTemplate
			"Orthographyrange",
			"ove",
			"Paeth", // PNG filter
			"Pausable",
			"Pcl",
			"Pcm",
			"Pdu",
			"Persistance",
			"Pesented",
			"Pfs", // acronym
			"Pkcs",
			"Placemark",
			"Playthrough",
			"Pointillize",
			"Polyline",
			"Popularimeter",
			"Prerolls",
			"Preseti",
			"Propogate",
			"Psec",
			"Pvrtc", // MTLBlitOption - PowerVR Texture Compression
			"Quaterniond",
			"Qura",
			"Reacquirer",
			"Reinvitation",
			"Reinvite",
			"Replayable",
			"Requestwith",
			"Rgb",
			"Rgba",
			"Roi",
			"Romm", // acronym: Reference Output Medium Metric
			"Rpa",
			"Rpn", // acronym
			"Rssi",
			"Rtsp",
			"Scn",
			"Sdk",
			"Sdtv", // acronym: Standard Definition Tele Vision
			"Seekable",
			"Shadable",
			"Sharegroup",
			"Siemen",
			"Sinh",
			"Sint", // as in "Signed Integer"
			"Slerp",
			"Slomo",
			"Smpte",
			"Snapshotter",
			"Snorm",
			"Sobel",
			"Spacei",
			"Sqrt",
			"Srgb",
			"Ssid",
			"Standarize",
			"Stateful",
			"Stateright",
			"Subbeat",
			"Subcardioid",
			"Subentities",
			"Subheadline",
			"Submesh",
			"Submeshes",
			"Subpixel",
			"Subsec",
			"Superentity",
			"Sym",
			"Synchronizable",
			"Tanh",
			"Texcoord",
			"Texel",
			"th",
			"Threadgroup",
			"Threadgroups",
			"Thumbstick",
			"Timelapse",
			"Timelapses",
			"Tls",
			"Tlv",
			"Toi",
			"Truncantion",
			"Tweening",
			"tx",
			"ty",
			"Udi",
			"Udp",
			"Unconfigured",
			"Undecodable",
			"Underrun",
			"Unorm",
			"Unprepare",
			"Unproject",
			"Uterance",
			"Utf",
			"Uti",
			"Varispeed",
			"Vergence",
			"Vnode",
			"Vpn",
			"Whitespaces",
			"Writeability",
			"Xpc",
			"xy",
			"Xyz",
			"yuvs",
			"yx",
			"yy",
			"Yyy",
#if !XAMCORE_2_0
			// classic only mistakes - that should not change anymore
			"Timetime",
			"Rectfrom",
			"Distancefrom",
			"Calendarc",
			"Negotiat",
			"Trus",
			"Placemarks",
			"Chage",
			"Elipse",
			"intptr",
			"rbool",
			"rint",
			"rfloat",
			"rdouble",
			"rintptr",
			"cgsize",
			"cgpoint",
			"cgrect",
			"nsrange",
			"stret",
			"monotouch",
			"xamarin",
			"Dimiss",
			"Owneroptions",
			"Delegat",
			"Nibfor",
			"Delegatequeue",
			"Sispatch",
#endif
		};

		// ease maintenance of the list
		HashSet<string> used = new HashSet<string> ();

		bool SkipAllowed (string typeName, string methodName, string typo)
		{
			if (allowed.Contains (typo)) {
				used.Add (typo);
				return true;
			}
			return false;
		}

		bool IsObsolete (MemberInfo mi)
		{
			if (mi == null)
				return false;
			if (mi.GetCustomAttributes<ObsoleteAttribute> (true).Any ())
				return true;
			return IsObsolete (mi.DeclaringType);
		}

		[Test]
		public void TypoTest ()
		{
			var types = Assembly.GetTypes ();
			int totalErrors = 0;
			foreach (Type t in types) {
				if (t.IsPublic && !IsObsolete (t)) {
					string txt = NameCleaner (t.Name);
					var typo = GetTypo (txt);
					if (typo.Length > 0 ) {
						if (!Skip (t, typo)) {
							ReportError ("Typo in TYPE: {0} - {1} ", t.Name, typo);
							totalErrors++;
						}
					}

					var fields = t.GetFields ();
					foreach (FieldInfo f in fields) {
					if (!f.IsPublic && !f.IsFamily && !IsObsolete (f))
							continue;
						
						txt = NameCleaner (f.Name);
						typo = GetTypo (txt);
						if (typo.Length > 0) {
							if (!Skip (f, typo)) {
								ReportError ("Typo in FIELD name: {0} - {1}, Type: {2}", f.Name, typo, t.Name);
								totalErrors++;
							}
						}
					}

					var methods = t.GetMethods ();
					foreach (MethodInfo m in methods) {
					if (!m.IsPublic && !m.IsFamily && !IsObsolete (m))
							continue;
						
						txt = NameCleaner (m.Name);
						typo = GetTypo (txt);
						if (typo.Length > 0) {
							if (!Skip (m, typo)) {
								ReportError ("Typo in METHOD name: {0} - {1}, Type: {2}", m.Name, typo, t.Name);
								totalErrors++;
							}
						}
#if false
						var parameters = m.GetParameters ();
						foreach (ParameterInfo p in parameters) {
							txt = NameCleaner (p.Name);
							typo = GetTypo (txt);
							if (typo.Length > 0) {
								ReportError ("Typo in PARAMETER Name: {0} - {1}, Method: {2}, Type: {3}", p.Name, typo, m.Name, t.Name);
								totalErrors++;
							}
						}
#endif
					}
				}
			}
#if false
			// ease removal of unrequired values (but needs to be checked for every profile)
			var unused = allowed.Except (used);
			foreach (var typo in unused)
				Console.WriteLine ("Unused entry \"{0}\"", typo);
#endif
			Assert.IsTrue ((totalErrors == 0), "We have {0} typos!", totalErrors);
		}

		public abstract string GetTypo (string txt);

		static StringBuilder clean = new StringBuilder ();

		static string NameCleaner (string name)
		{
			clean.Clear ();
			foreach (char c in name) {
				if (Char.IsUpper (c)) {
					clean.Append (' ').Append (c);
					continue;
				}
				if (Char.IsDigit (c)) {
					clean.Append (' ');
					continue;
				}
				switch (c) {
				case '<':
				case '>':
				case '_':
					clean.Append (' ');
					break;
				default:
					clean.Append (c);
					break;
				}
			}
			return clean.ToString ();
		}
	}
}