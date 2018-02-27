using System.Collections;
using UnityEngine;

[CreateAssetMenu()]
public class MeshSettings : UpdatableData {
	

	public const int numSupportedLODs = 5;//1 2 4 6 8
	public const int numSupportedChunkSizes = 9;
	public const int numFlatShadedSupportedChunkSizes = 3;
	public static readonly int[] supportedChunkSizes = {48,72,96,120,144,168,192,216,240};

	public float meshScale = 10f;
	public bool useFlatShading;

	[Range(0,numSupportedChunkSizes-1)]
	public int chunkSizeIndex;
	[Range(0,numFlatShadedSupportedChunkSizes-1)]
	public int flatShadedChunkSizeIndex;

	// num verts per line of mesh rendered at LOD = 0. Includes the 2 extra verts that are excluded from final mesh, but used for calculating normals
	public int numVertsPerline{
		get {
			return supportedChunkSizes[(useFlatShading)?flatShadedChunkSizeIndex:chunkSizeIndex]+1;
		}
	}

	public float meshWorldSize {
		get {
			return (numVertsPerline - 3) * meshScale;
		}
	}

}
