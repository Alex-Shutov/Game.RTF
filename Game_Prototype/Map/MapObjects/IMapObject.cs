using System;
using System.Windows.Forms;

namespace Game_Prototype
{
    public interface IMapObject
    {
        public bool isWorking { get; set; }
        public bool WasPressed { get; set; }
        public bool isCollide { get; set; }
        public PictureBox picture { get; set; }
        private MapObjDelegate[] Methods => throw new NotImplementedException();

        public void MakeAction();

        public void OnCollide();

        public void DeCollide();
        private void CreateRandomFilling()
        {
            throw new NotImplementedException();
        }
    }
}
