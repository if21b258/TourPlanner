﻿using TourPlannerModel;

namespace TourPlannerBL.Service
{
    internal interface IFileService
    {
        public string GetFileDirectory();
        public string GetFilePath(TourModel tour);
        public void SaveImageToFile(byte[] mapImage, TourModel tour);
        public void DeleteImageFolder();
        public void DeleteImage(TourModel tour);
    }
}
