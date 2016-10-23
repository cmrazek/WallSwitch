using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallSwitch
{
	static class Global
	{
		#region Ratings
		public static event EventHandler<RatingUpdatedEventArgs> RatingUpdated;

		public class RatingUpdatedEventArgs : EventArgs
		{
			public string Location { get; private set; }
			public int Rating { get; private set; }

			public RatingUpdatedEventArgs(string location, int rating)
			{
				Location = location;
				Rating = rating;
			}
		}

		public static void UpdateRating(string location, int rating)
		{
			if (string.IsNullOrEmpty(location)) throw new ArgumentNullException(nameof(location));
			if (rating < 0 || rating > 5) throw new ArgumentOutOfRangeException(nameof(rating));

			RatingUpdated?.Invoke(null, new RatingUpdatedEventArgs(location, rating));
		}
		#endregion

		#region Delete File
		public static event EventHandler<DeleteFileEventArgs> FileDeleted;

		public class DeleteFileEventArgs : EventArgs
		{
			public string LocationOnDisk { get; private set; }

			public DeleteFileEventArgs(string locationOnDisk)
			{
				LocationOnDisk = locationOnDisk;
			}
		}

		public static void OnFileDeleted(string locationOnDisk)
		{
			FileDeleted?.Invoke(null, new DeleteFileEventArgs(locationOnDisk));
		}
		#endregion
	}
}
