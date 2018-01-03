using UnityEngine;
using System.Collections.Generic;

public class MouseController : MonoBehaviour
{
	public GameObject circleCursorPrefab;

	Vector3 lastFramePosition;
	Vector3 currFramePosition;

	Vector3 dragStartPosition;
	private List<GameObject> dragPreviewGameObjects;

	void Start()
	{
		dragPreviewGameObjects = new List<GameObject>();
	}

	void Update()
	{
		currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currFramePosition.z = 0;

		//UpdateCursor();
		UpdateDragging();
		UpdateCameraMovement();

		lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lastFramePosition.z = 0;
	}

	void UpdateCursor()
	{
//		Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
//
//
//		if (tileUnderMouse != null)
//		{
//			circleCursorPrefab.SetActive(true);	
//			Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
//			circleCursorPrefab.transform.position = cursorPosition;
//		}
//		else
//		{
//			circleCursorPrefab.SetActive(false);
//		}
	}

	void UpdateDragging()
	{
		if (Input.GetMouseButtonDown(0))
		{
			dragStartPosition = currFramePosition;
		}

		int start_x = Mathf.FloorToInt(dragStartPosition.x);
		int end_x = Mathf.FloorToInt(currFramePosition.x);
		int start_y = Mathf.FloorToInt(dragStartPosition.y);
		int end_y = Mathf.FloorToInt(currFramePosition.y);

		if (end_x < start_x)
		{
			int tmp = end_x;
			end_x = start_x;
			start_x = tmp;
		}
		if (end_y < start_y)
		{
			int tmp = end_y;
			end_y = start_y;
			start_y = tmp;
		}

		while (dragPreviewGameObjects.Count > 0)
		{
			GameObject go = dragPreviewGameObjects[0];
			dragPreviewGameObjects.RemoveAt(0);
			Pooling.Despawn(go);
		}

		if (Input.GetMouseButton(0))
		{
			for (int x = start_x; x <= end_x; x++)
			{
				for (int y = start_y; y <= end_y; y++)
				{
					Tile t = WorldController.Instance.World.GetTileAt(x, y);

					if (t != null)
					{
						GameObject go = Pooling.Spawn(circleCursorPrefab, new Vector3(x, y, 0), Quaternion.identity);
						go.transform.SetParent(this.transform, true);
						dragPreviewGameObjects.Add(go);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			for (int x = start_x; x <= end_x; x++)
			{
				for (int y = start_y; y <= end_y; y++)
				{
					Tile t = WorldController.Instance.World.GetTileAt(x, y);
					if (t != null)
					{
						t.Type = Tile.TileType.Floor;
					}
				}
			}
		}
	}

	void UpdateCameraMovement()
	{
		if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
			Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
			Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
		}
	}
}


/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	public GameObject CircleCursor;
	private Vector3 lastFramePosition;
	private Vector3 dragStartPosition;

	void Start()
	{
	}

	void Update()
	{
		Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currFramePosition.z = 0;

		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);

		if (tileUnderMouse != null)
		{
			CircleCursor.SetActive(true);
			Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
			CircleCursor.transform.position = cursorPosition;
		}
		else
		{
			CircleCursor.SetActive(false);
		}

		if (Input.GetMouseButtonDown(0))
		{
			dragStartPosition = currFramePosition;
		}

		if (Input.GetMouseButtonUp(0))
		{
			int start_x = Mathf.FloorToInt(dragStartPosition.x);
			int end_x = Mathf.FloorToInt(currFramePosition.x);

			if (end_x < start_x)
			{
				int tmp = end_x;
				end_x = start_x;
				start_x = tmp;
			}

			int start_y = Mathf.FloorToInt(dragStartPosition.y);
			int end_y = Mathf.FloorToInt(currFramePosition.y);

			if (end_y < start_y)
			{
				int tmp = end_y;
				end_y = start_y;
				start_y = tmp;
			}

			for (int x = start_x; x <= end_x; x++)
			{
				for (int y = start_y; y <= end_y; y++)
				{
					Tile t = WorldController.Instance.World.GetTileAT(x, y);

					if (t != null)
					{
						t.Type = Tile.TileType.Floor;
					}
				}
			}
		}
		
		if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
		{
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
		}
		
		lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		lastFramePosition.z = 0;
	}

	Tile GetTileAtWorldCoord(Vector3 coord)
	{
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);
		return WorldController.Instance.World.GetTileAT(x, y);
	}
}
*/


