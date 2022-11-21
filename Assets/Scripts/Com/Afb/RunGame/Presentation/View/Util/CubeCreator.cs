using UnityEngine;

namespace Com.Afb.RunGame.Presentation.View.Util {
    public static class CubeCreator {
        // Public Funtions
        public static void GenerateCube(Mesh mesh, Vector3 size) {
            // Vericies
            Vector3[] vertices = GetVertices(size);

            // Normals
            Vector3[] normals = GetNormals();

            // UV
            Vector2[] uvs = GetUVs();

            // Triangles
            int[] triangles = GetTriangles();

            // Set Mesh
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.Optimize();
        }

        // Private Funtions
        private static Vector3[] GetVertices(Vector3 size) {
            float width = size.x;
            float height = size.y;
            float length = size.z;

            Vector3[] c = new Vector3[8];

            c[0] = new Vector3(-width * .5f, -height * .5f, length * .5f);
            c[1] = new Vector3(width * .5f, -height * .5f, length * .5f);
            c[2] = new Vector3(width * .5f, -height * .5f, -length * .5f);
            c[3] = new Vector3(-width * .5f, -height * .5f, -length * .5f);

            c[4] = new Vector3(-width * .5f, height * .5f, length * .5f);
            c[5] = new Vector3(width * .5f, height * .5f, length * .5f);
            c[6] = new Vector3(width * .5f, height * .5f, -length * .5f);
            c[7] = new Vector3(-width * .5f, height * .5f, -length * .5f);


            return new Vector3[] {
                c[0], c[1], c[2], c[3], // Bottom
	            c[7], c[4], c[0], c[3], // Left
	            c[4], c[5], c[1], c[0], // Front
	            c[6], c[7], c[3], c[2], // Back
	            c[5], c[6], c[2], c[1], // Right
	            c[7], c[6], c[5], c[4]  // Top
            };
        }

        private static Vector3[] GetNormals() {
            Vector3 up = Vector3.up;
            Vector3 down = Vector3.down;
            Vector3 forward = Vector3.forward;
            Vector3 back = Vector3.back;
            Vector3 left = Vector3.left;
            Vector3 right = Vector3.right;

            return new Vector3[] {
                down, down, down, down,             // Bottom
	            left, left, left, left,             // Left
	            forward, forward, forward, forward,	// Front
	            back, back, back, back,             // Back
	            right, right, right, right,         // Right
	            up, up, up, up                      // Top
            };
        }

        private static Vector2[] GetUVs() {
            Vector2 uv00 = new Vector2(0f, 0f);
            Vector2 uv10 = new Vector2(1f, 0f);
            Vector2 uv01 = new Vector2(0f, 1f);
            Vector2 uv11 = new Vector2(1f, 1f);

            return new Vector2[] {
                uv11, uv01, uv00, uv10, // Bottom
	            uv11, uv01, uv00, uv10, // Left
	            uv11, uv01, uv00, uv10, // Front
	            uv11, uv01, uv00, uv10, // Back	        
	            uv11, uv01, uv00, uv10, // Right 
	            uv11, uv01, uv00, uv10  // Top
            };
        }

        private static int[] GetTriangles() {
            return new int[] {
                3, 1, 0,        3, 2, 1,        // Bottom	
	            7, 5, 4,        7, 6, 5,        // Left
	            11, 9, 8,       11, 10, 9,      // Front
	            15, 13, 12,     15, 14, 13,     // Back
	            19, 17, 16,     19, 18, 17,	    // Right
	            23, 21, 20,     23, 22, 21,     // Top
            };
        }
    }
}
