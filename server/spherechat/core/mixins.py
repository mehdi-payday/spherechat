
class ObservableManagerMixin(object):
    _observers = []

    @classmethod
    def register_observer(cls, observer):
        cls._observers.append(observer)

    @classmethod
    def notify_observers(cls, method, *args, **kwargs):
        for observer in cls._observers:
            getattr(observer, method)(*args, **kwargs)  

    def create(self, **kwargs):
        instance = super(ObservableManagerMixin, self).create(**kwargs)
        
        self.__class__.notify_observers("on_create", instance)

        return instance
