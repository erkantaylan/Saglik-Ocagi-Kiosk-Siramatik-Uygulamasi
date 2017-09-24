namespace HealtCare.Common.Aggregator {

    public interface IEventAggregator {
        void PublishEvent<TEventType>(TEventType eventToPublish);

        void SubsribeEvent(object subscriber);
    }

}