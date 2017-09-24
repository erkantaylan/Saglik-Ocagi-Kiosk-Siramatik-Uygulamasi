namespace HealtCare.Common.Aggregator {

    public interface ISubscriber<in TEventType> {
        void OnEventHandler(TEventType e);
    }

}