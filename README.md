# Loading System

Dynamically load Game Objects into the Scene using Addressables.

## How To Use

Make surre the prefab you want to load is an Addressable one:

![Addressable Prefab](/Docs~/AddressablePrefab.png)

Place into your scene a GameObject that will load this prefab, attaching the [AddressablesLoader](/Runtime/AddressablesLoader.cs) component on it.

![Addressables Loader](/Docs~/AddressablesLoader.png)

Set the **Prefab** and **Place** fields on it. Don't forget to add any 3D collider as a trigger on this GameObject (a warning log will be fired if forget).

Inside your Player or Camera GameObject, put a (Loader)[/Runtime/Loader.cs] component on it.

![Loader](/Docs~/Loader.png)

Don't forget to set the Collision field and add any 3D Collider.

The Collider should also be a trigger and it'll load any [ILoadable](/Runtime/ILoadable.cs) implementation (like AddressablesLoader) when entering on its area; unload when exiting.

![Preview](/Docs~/Preview.gif)

## Installation

### Using the Git URL

You will need a **Git client** installed on your computer with the Path variable already set and the correct git credentials to 1M Bits Horde.

- In this repo, go to Code button, select SSH and copy the URL.
- In Unity, use the **Package Manager** "Add package from git URL..." feature and paste the URL.
- Set the version adding the suffix `#[x.y.z]` at URL

---

**1 Million Bits Horde**

[Website](https://www.1mbitshorde.com) -
[GitHub](https://github.com/1mbitshorde) -
[LinkedIn](https://www.linkedin.com/company/1m-bits-horde)
