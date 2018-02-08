using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class NoiseData : UpdatableData {
	public Noise.NormalizeMode normalizeMode; 
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistence;
	public float lacunarity;
	public float plateauAdjustment;

	public int seed;
	public Vector2 offset;


	protected override void OnValidate(){

		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
		if (plateauAdjustment < 1) {
			plateauAdjustment = 1;
		}

		base.OnValidate();
	}

}
