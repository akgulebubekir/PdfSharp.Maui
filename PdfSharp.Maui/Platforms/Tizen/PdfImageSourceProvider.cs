using System.IO;
using System.Threading.Tasks;
using System;

namespace PdfSharp.Maui.Utils;

internal static partial class PdfImageSourceProvider
{
  static partial void RegisterImageSourceImpl(Func<string, Task<Stream>> fileSystemStreamProvider)
  {
    throw new InvalidOperationException("ImageSource does not supported by Tizen Platform");
  }

}