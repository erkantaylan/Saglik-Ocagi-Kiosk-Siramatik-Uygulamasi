namespace HealtCare.Common.Models {

    public class Patient {
        public Patient(int doctorId, string type, int no) {
            No = no;
            DoctorId = doctorId;
            Type = type;
        }

        public int No { get; set; }

        public int DoctorId { get; set; }
        public string Type { get; set; }
    }

}