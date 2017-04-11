
class ObservableManagerMixin(object):
    def __init__(self, *args, **kwargs):
        super(ObservableManagerMixin, self).__init__(*args, **kwargs)
        self._observers = []

    def register_observer(self, observer):
        self._observers.append(observer)

    def notify_observers(self, method, *args, **kwargs):
        for observer in self._observers:
            getattr(observer, method)(*args, **kwargs)  

    def create(self, **kwargs):
        instance = super(ObservableManagerMixin, self).create(**kwargs)
        
        self.notify_observers("on_create", instance)

        return instance
