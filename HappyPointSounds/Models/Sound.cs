using System.IO;

namespace HappyPointSounds.Models
{
	class Sound
	{
		public FileInfo FileName { get; private set; }
		public string Keyword { get; private set; }


		public Sound(FileInfo fileName) => FileName = fileName;

		internal void SetKeyword(string keyword) => Keyword = keyword;
		internal void SetFileName(FileInfo filename) => FileName = filename;
	}
}
