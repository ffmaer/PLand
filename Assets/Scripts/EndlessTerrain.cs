﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour {

	public const float maxViewDist = 450;
	public Transform viewer;

	public static Vector2 viewerPosition;
	int chunkSize;
	int chunkVisibleInViewDst;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunkVisibleLastUpdate = new List<TerrainChunk>();

	void Start(){
		chunkSize = MapGenerator.mapChunkSize - 1;
		chunkVisibleInViewDst = Mathf.RoundToInt (maxViewDist / chunkSize);
	}

	void Update(){
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.z);
		UpdateVisibleChunks ();
	}

	void UpdateVisibleChunks(){

		for (int i = 0; i < terrainChunkVisibleLastUpdate.Count; i++) {
			terrainChunkVisibleLastUpdate [i].SetVisible (false);
		}
		terrainChunkVisibleLastUpdate.Clear ();

		int CurrentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int CurrentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		for(int yOffset=-chunkVisibleInViewDst; yOffset<=chunkVisibleInViewDst; yOffset++){
			for(int xOffset=-chunkVisibleInViewDst; xOffset<=chunkVisibleInViewDst; xOffset++){
				Vector2 viewedChunkCoord = new Vector2 (CurrentChunkCoordX + xOffset, CurrentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
					terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk();
					if (terrainChunkDictionary [viewedChunkCoord].IsVisible ()) {
						terrainChunkVisibleLastUpdate.Add (terrainChunkDictionary [viewedChunkCoord]);
					}
				} else {
					terrainChunkDictionary.Add (viewedChunkCoord, new TerrainChunk (viewedChunkCoord, chunkSize, viewer));
				}

			}
		}

			
	}

	public class TerrainChunk{

		GameObject meshObject;
		Vector2 position;
		Bounds bounds;



		public TerrainChunk(Vector2 coord, int size, Transform parent){
			position = coord * size;
			bounds = new Bounds (position, Vector2.one * size);
			Vector3 positionV3 = new Vector3 (position.x, 0, position.y);

			meshObject = GameObject.CreatePrimitive (PrimitiveType.Plane);
			meshObject.transform.position = positionV3;
			meshObject.transform.localScale = Vector3.one * size / 10f;
			meshObject.transform.parent = parent;
			SetVisible(false);
		}

		public void UpdateTerrainChunk(){
			float viewerDstFromNearestEdge = Mathf.Sqrt (bounds.SqrDistance (viewerPosition));
			bool visible = viewerDstFromNearestEdge <= maxViewDist;
			SetVisible(visible);

		}

		public void SetVisible(bool visible){
			meshObject.SetActive (visible);
		}

		public bool IsVisible(){
			return meshObject.activeSelf;
		}
	}
}
