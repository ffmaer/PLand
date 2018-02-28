using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeightMapGenerator {

	public static HeightMap GenerateHeightMap(int width, int height, HeightMapSettings settings, Vector2 sampleCenter){
		float[,] values = Noise.GenerateNoiseMap (width, height, settings.noiseSettings, sampleCenter);
		AnimationCurve heightCurve_threadsafe = new AnimationCurve (settings.heightCurve.keys);
	

		for(int i=0; i<width; i++){
			for(int j=0; j<height; j++){
				values [i, j] = heightCurve_threadsafe.Evaluate (values[i,j])*settings.heightMultiplier;


			}
		}
		return new HeightMap (values);
	}
}

public struct HeightMap{
	public readonly float[,] values;

	public HeightMap(float[,] values){
		this.values = values;

	}
}