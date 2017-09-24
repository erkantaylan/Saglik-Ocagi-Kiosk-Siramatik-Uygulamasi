using HealtCare.Common.Aggregator;

namespace HealtCare.Common.Models {

    public class LoginResult {
        public IEventAggregator EventAggregator { get; set; }
        public Doctor Doctor { get; set; }
    }

}