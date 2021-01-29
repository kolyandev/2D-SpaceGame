using UnityEngine;
using Random = System.Random;

public class FlyingObject : MonoBehaviour {


  [SerializeField] private string target    = "Player";
  private                  int    speed     = 3;
  private                  int    rotateOn  = 20;
  private                  float  _lifeTime = 10f;
  private                  bool   _isGood;

  private void Start() {
    GenerateSpeed();
    GenerateQuality();
  }

  private void Update() {
    Move();
    RunLifeTime();
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.gameObject.CompareTag(target)) return;

    // TODO: handler touching with player: up/de- grade
    Destroy(this.gameObject);
  }

  private void GenerateSpeed() {
    speed    = new Random().Next(2,  6);
    rotateOn = new Random().Next(10, 26);

    // size depending from speed;
    // switch (speed) {
    //   case 2:
    //   case 3:
    //     this.gameObject.transform.localScale = new Vector3(2, 2, 2);
    //
    //     break;
    //   case 4:
    //     this.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //
    //     break;
    //   case 5:
    //     this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    //
    //     break;
    // }
  }

  private void GenerateQuality() {
    _isGood = new Random().Next(0, 2) == 1;

    // Example behaviour of sprite from object's quality
    this.gameObject.GetComponent<SpriteRenderer>().color =
      _isGood
        ? new Color(.5f, 1f,  .5f)
        : new Color(1f,  .5f, .5f);
  }

  private void Move() {
    Quaternion rotation = this.transform.rotation;

    this.transform.Translate(
      Vector2.left * (speed * Time.deltaTime),
      Space.World
    );

    this.transform.Rotate(
      rotation.x,
      rotation.y,
      (rotation.z + rotateOn) * Time.deltaTime,
      Space.Self
    );
  }

  private void RunLifeTime() {
    _lifeTime -= Time.deltaTime;
    if (_lifeTime <= 0) Destroy(this.gameObject);
  }


}
