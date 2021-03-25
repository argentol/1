﻿using UnityEngine;

public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	private Pipe[] pipes;

	private int WhitePipe = 1;

	private void Awake () {
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
			pipe.transform.SetParent(transform, false);
			pipe.Generate(WhitePipe);
			WhitePipe++;
			print(WhitePipe);
			if (i > 0) {
				pipe.AlignWith(pipes[i - 1]);
			}
		}
		AlignNextPipeWithOrigin();
	}

	public Pipe SetupFirstPipe () {
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe () {
		ShiftPipes();
		AlignNextPipeWithOrigin();
		WhitePipe++;
		pipes[pipes.Length - 1].Generate(WhitePipe);
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

	private void AlignNextPipeWithOrigin () {
		Transform transformToAlign = pipes[1].transform;
		for (int i = 0; i < pipes.Length; i++)
		{
			if (i != 1)
			{
				pipes[i].transform.SetParent(transformToAlign);
			}
		}
		if (WhitePipe % 2 == 0)
		{
			Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
			material.color = new Color(Random.value, Random.value, Random.value, 1);
		}
		else
        {
			Material material = pipes[pipes.Length - 1].GetComponent<Renderer>().material;
			material.color = Color.white;
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