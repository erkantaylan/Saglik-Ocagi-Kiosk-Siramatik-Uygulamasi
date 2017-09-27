namespace HealtCare.Common.Models {

    public class Patient {
        public Patient(int doctorId, string type) {
            No = ++LastNo;
            DoctorId = doctorId;
            Type = type;
        }

        public static int LastNo { get; set; }

        public int No { get; set; }

        public int DoctorId { get; set; }
        public string Type { get; set; }
    }

}