using UnityEngine;
using UnityEngine.UI;
public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	private Pipe[] pipes;

    private int WhitePipe = 2;


    public Button Button1;
	public Button Button2;
	public Button Button3;

	public GameObject cheating;

	public Material Color1;
	public Material Color2;
	public Material Color3;

	public Material Color1Button;
	public Material Color2Button;
	public Material Color3Button;


	int renewColors;

	bool StartOfGame = false;
	private int newLevel = 0;

	bool WhiteRepeat = false;
	public bool NewLevel = true;
	private void Awake () {
		randomizeOfColors();
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
			pipe.transform.SetParent(transform, false);
			pipe.Generate(WhitePipe,NewLevel);
            WhitePipe++;
            if (i > 0) {
				pipe.AlignWith(pipes[i - 1]);
			}
		}
  //      Color1.color = new Color(Random.value, Random.value, Random.value, 1);
		//Color2.color = new Color(Random.value, Random.value, Random.value, 1);
		//Color3.color = new Color(Random.value, Random.value, Random.value, 1);
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
        //randomizeOfColors();
		ColoringOfButtons();
    }

	
    void FixedUpdate()
    {
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.black) /*&& (pipes[1].GetComponent<Renderer>().material.color == Color.white)*/)
		{
			print("DADA");
			ColoringOfButtons();
		}
		if (newLevel == 0)
		{
			//if(pipes[1].GetComponent<Renderer>().material.name == cheating.GetComponent<Renderer>().material.name)
   //         {
   //             Button1.image.color = Color1Button.color;
   //             Button2.image.color = Color2Button.color;
   //             Button3.image.color = Color3Button.color;
   //         }
			randomizeOfColors();
			newLevel = 10;
			NewLevel = true;
		}
		if ((pipes[1].GetComponent<Renderer>().material.color == Color.white) && (WhiteRepeat == false))
		{
			newLevel--;
			WhiteRepeat = true;
			print("zxc");
			NewLevel = false;
		}
		if (!(pipes[1].GetComponent<Renderer>().material.color == Color.white))
		{
			WhiteRepeat = false;
			NewLevel = false;
		}
		print(newLevel);
	}

    void randomizeOfColors()
    {
        Color1.color = new Color(Random.value, Random.value, Random.value, 1);
        Color2.color = new Color(Random.value, Random.value, Random.value, 1);
        Color3.color = new Color(Random.value, Random.value, Random.value, 1);
        //Color1Button.color = new Color(1, 1, 1, 1);
        //Color2Button.color = new Color(1, 1, 0, 1);
        //Color3Button.color = new Color(0, 1, 1, 1);
        Color1Button.color = Color1.color;
        Color2Button.color = Color2.color;
        Color3Button.color = Color3.color;

    }

    public Pipe SetupFirstPipe () {
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe () {
		ShiftPipes();
		AlignNextPipeWithOrigin(WhitePipe, newLevel);
        WhitePipe++;
        pipes[pipes.Length - 1].Generate(WhitePipe, NewLevel);
		pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	private void ShiftPipes () {
		Pipe temp = pipes[0];
		for (int i = 1; i < pipes.Length; i++) {
			pipes[i - 1] = pipes[i];
		}
		pipes[pipes.Length - 1] = temp;
	}

	int RandomMaterial = 1;

	private void ColoringOfButtons()
    {
		Button1.image.color = Color1Button.color;
		Button2.image.color = Color2Button.color;
		Button3.image.color = Color3Button.color;
	}
	private void AlignNextPipeWithOrigin (int WhitePipe, int newLevel) {
		Transform transformToAlign = pipes[1].transform;
		for (int i = 0; i < pipes.Length; i++)
		{
			if (i != 1)
			{
				pipes[i].transform.SetParent(transformToAlign);
			}
		}
		if (newLevel == 0 )
		{
			Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
			material.color = Color.black;
			NewLevel = false;
		}
		else
		{
			if (WhitePipe % 2 == 0)
			{
				Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
				RandomMaterial = Random.Range(1, 4);
				if (RandomMaterial == 1)
					material.color = Color1.color;
				if (RandomMaterial == 2)
					material.color = Color2.color; 
				if (RandomMaterial == 3)
					material.color = Color3.color; 	
			}
			else
			{
				Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
				material.color = Color.white;
			}
			//WhitePipe++;
		}

        transformToAlign.localPosition = Vector3.zero;
		transformToAlign.localRotation = Quaternion.identity;

		for (int i = 0; i < pipes.Length; i++)
		{
			if (i != 1)
			{
				pipes[i].transform.SetParent(transform);
			}
		}
	}
}