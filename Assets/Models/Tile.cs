using UnityEngine;
using System.Collections;
using System;

public class Tile
{
	public enum TileType
	{
		Empty,
		Floor
	}

	private TileType _type = TileType.Empty;

	public TileType Type
	{
		get { return _type; }
		set
		{
			TileType oldType = _type;
			_type = value;
			if (cbTileTypeChanged != null && oldType != _type)
				cbTileTypeChanged(this);
		}
	}

	private LooseObject looseObject;
	private InstalledObject installedObject;
	private World world;
	private int x;
	private int y;

	public int X
	{
		get { return x; }
	}

	public int Y
	{
		get { return y; }
	}

	private Action<Tile> cbTileTypeChanged;

	public Tile(World world, int x, int y)
	{
		this.world = world;
		this.x = x;
		this.y = y;
	}

	public void RegisterTileTypeChangedCallback(Action<Tile> callback)
	{
		cbTileTypeChanged += callback;
	}

	public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
	{
		cbTileTypeChanged -= callback;
	}
}