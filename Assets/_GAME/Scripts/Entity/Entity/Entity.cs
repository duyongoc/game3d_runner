﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("[Setting]")]
    public float distance = 5f;
    public List<Entity> enemies, detected;

}
