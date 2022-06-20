
# Toca Boca Assignment

Hi, my name is Jose de la Iglesia and this is the repository that includes all related with the Assignment for the Senior Programmer position.

## The task
The task is clear, create an Audio System able to add sound and music to the game, having to support:
* Pedestrian pickup
* Background music
* Collisions
* Success sing upon delivery

## Event System
First of all we need a way to detect when this events are triggered because the current code doesn´t expose those. For this I decided to keep the things simple
and I use a really basic system made of ScriptableObjects, following this talk in the Unite 2017: [Game Architecture with Scriptable Objects](https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity).

I use Clean Arquitecture and follow the SOLID principles in my daily basics at work but for this assigment I think that some of the infraestructure would 
result in an increase of complexity so I tried to keep it clean but simple. 

All the scripts related to the assigment are inside the AudioSystem folder in the Assets folder of the project. There you can find some 
subfolders with the ScriptableObjects that defines the core concepts of the event system:
* GameEvent
* AudioEventListener

Both can be create through the Create Asset Menu : Create -> TocaAssignment -> GameEvent / AudioEventListener. As you can check, there are
four GameEvents created in the project:
* CarCollision
* GameStarted
* NPCPick
* NPCDelivery

Extend the game with more events is as easy as creating a new ScriptableObjects of the type GameEvent and Raise the event in our code
when the events triggers. I did exactly this in our example. I modified the current code adding a reference in the script of the GameEvent
and call the Raise method when the event triggers as you can see in this example:
```
public class NPC : MonoBehaviour
{
    private ParticleSystem pickupParticles;
    public GameEvent pickupGameEvent;

    void Awake()
    {
        pickupParticles = GetComponentInChildren<ParticleSystem>();
    }
    public void SetParticlesActive(bool isActive)
    {
        pickupParticles.gameObject.SetActive(isActive);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment)
            {
                GameObject.Find("GameRunner").GetComponent<GameRunner>().SetLocationForNPC(this);
                GameObject.Find("GameRunner").GetComponent<GameRunner>().isOnAssignment = true;
                GameObject.Find("GameRunner").GetComponent<GameRunner>().SetAllNPCParticles(false);
                Destroy(gameObject);
                pickupGameEvent.Raise();
            }
        }
    }
```

On the other hand, we have to listen this events. For this task I made an interface called IGameEventListener and because we only need
audio related listeners, there is only one implementation of this interface, the AudioEventListener.
```
public interface IGameEventListener
    {
        public void OnEventRaised();
        public void RegisterEvent();
        public void UnRegisterEvent();
        public GameEvent gameEvent { get; set; }
    }
```

```
[Serializable]
    [CreateAssetMenu(menuName = "TocaAssignment/AudioEventListener")]
    public class AudioEventListener : ScriptableObject,IGameEventListener
    {
        public AudioSystem audioSystem;
        
        public void RegisterEvent()
        {
            _gameEvent.RegisterListener(this);
        }

        public void UnRegisterEvent()
        {
            _gameEvent.UnregisterListener(this);
        }
        public AudioPlayable audioPlayable;
        [SerializeField]
        private GameEvent _gameEvent;

        public void OnEventRaised()
        {
            audioSystem.Play(audioPlayable.GetAudioInfo());
        }

        public GameEvent gameEvent
        {
            get => _gameEvent;
            set => _gameEvent = value;
        }
    }
```

Here we see how we have methods to suscribe and desuscribe from the event, one callback that actually plays the audio and some references but here ends all 
related to the event system created to be able to trigger game events.







## References
One of the hardest things to resolve in a project are the References and normally that is an indicator of systems that could be modelled
in a better way or directly code smells. In my projects I use Dependency Injection with an external library (Extendjet) but in this project
I just use direct Editor references (because as i read in a technical paper, Unity serve as Dependency Container in the editor) and
when I need some logic that is encapsulated in a class, like what happen with the AudioSystem and the Play Method, I use it like a service
but instead of creating a Singleton that would be a bad practice, I apply the single responsability principle, the AudioEventListener 
is not responsible for looking for their dependencies, he is responsible for listening to their events so I inject the audioSystem Dependency
in the AudioSystem who has all the audiolisteners when they are added.

```
private void InjectReferencesAndRegisterEvents()
{
    foreach (var listener in GameEventListeners)
    {
        listener.audioSystem = this;
        listener.RegisterEvent();
    }
}
public void Awake()
{
    InjectReferencesAndRegisterEvents();
}
```

As I said, in a production project I would use some Dependency Injection system that would be responsible of ressolving all the references.

## Audio System
The system is build from many parts:
### Audio System [Monobehaviour] : 
Responsible of containing the audiolisteners and provide support for the audiosources responsible of playing the sound.
I use the ObjectPool feature that Unity bring to us in the 2021 version:
```
private void CreatePool()
{
    _audioSourcesPool = new ObjectPool<TocaAudioSource>(createFunc: () => GameObject.Instantiate<TocaAudioSource>(audioSourcePrefab,transform), 
        actionOnGet: (obj) => obj.gameObject.SetActive(true), 
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj), 
        defaultCapacity: POOLSIZE);
}
```
 This way when someone call the Play method, there is a pool of audiosources that are tracked to play that sound and when the sound are 
 completed, they goes back to the pool.

 ### Audio Event Listener [ScriptableObject] : 
 As we saw before, is the class responsible of listening to the gameevent and call the play method of the AudioPlayable interface

 ### AudioPlayable [interface]:
This interface defines the AudioInfo that is going to be played. I create only two implementations of this, the random audioclip from a list (used for the collisions)
and the single audio for the rest of the sound clips.

### AudioInfo [ScriptableObject]:
This class encapsulate an audio clip and all the needed information that the audioSystem needs to play the sound. In this case I only 
use the clip, a boolean that defines if it has to be played in a loop (for the background music) and the audioMixerGroup that divide
the sounds in different groups that can be modified independently, this is great if you want to have different volumes for the music
and the sounds of the effects.
```
[CreateAssetMenu(menuName = "TocaAssignment/AudioInfo")]
public class AudioInfo : ScriptableObject
{
    public AudioClip audioClip;
    public bool loop;
    public AudioMixerGroup audioMixerGroup;
}
```

## Editor UI
To create GameEvents or new Audios for the game, the user only need to create those assets through the Create menu in the assets
folder but to show how I would make this in a production environment, I use the new UI Toolkit to create an Editor for the AudioSystem

![AudioSystem Editor](https://github.com/ph0b0ss/TocaAssignment/blob/main/audioSystem.PNG)

Here you can see that is easy to create a new listener to a GameEvent and assign a sound that will be played reacting to that event.
The GameEvent dropDown is a custom component that i make from scratch and track dinamically the GameEvents in the project and update 
the list. If you change the event in that dropDown of the current listeners its update on runtime also, as all related with this AudioSystem
that works also in build.