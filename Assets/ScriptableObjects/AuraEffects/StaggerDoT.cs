using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/AuraEffect/StaggerDoT")]
public class StaggerDot : AuraEffect
{
    private List<StaggerApplication> applications;

    public override void OnAdd()
    {
        base.OnAdd();

        applications = new List<StaggerApplication>();
        applications.Add(new StaggerApplication(aura.magnitude));

        RefreshMagnitude();
    }

    public override void Tick()
    {
        base.Tick();

        TickApplications();
    }

    public override void AddStack(float amount)
    {
        base.AddStack(amount);

        applications.Add(new StaggerApplication(amount));

        RefreshMagnitude();
    }

    private void TickApplications()
    {
        RefreshMagnitude();

        for (int i = applications.Count - 1; i >= 0; i--)
        {
            var application = applications[i];

            // Don't send events or else Stagger will absorb it
            aura.owner.TakeDamage(application.tickDamage, false);

            // Tick down and remove expired applications
            application.remainingDuration--;
            if(application.remainingDuration <= 0)
            {
                applications.RemoveAt(i);
            }
        }
    }

    private void RefreshMagnitude()
    {
        aura.magnitude = applications.Sum(a => a.magnitude);
    }
}

public class StaggerApplication
{
    public float magnitude;
    public int remainingDuration;

    public float tickDamage { get => magnitude / Stagger.MaxStaggerDuration; }
    public float remainingDamage { get => tickDamage * remainingDuration; }

    public StaggerApplication(float newMagnitude)
    {
        remainingDuration = Stagger.MaxStaggerDuration;
        magnitude = newMagnitude;
    }

    public void Tick()
    {
        remainingDuration--;
    }
}