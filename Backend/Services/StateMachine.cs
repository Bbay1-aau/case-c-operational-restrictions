namespace Backend.Services;

using Backend.Models.DTOs;
using Backend.Models.Enum;
using Backend.Models.Entities;

using Npgsql.Internal;

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

public class StateMachine
{
    // Function to call if you wish to transition
    public void Transition(OperationalRestriction opRes)
    {
        bool requirements = Require(opRes);

        // This should use "UpdateByIdRequest.cs"
        if (requirements) 
        {
            opRes.State = opRes.State switch
        {
            RestrictionState.ACTIVE          => RestrictionState.PENDING_CANCELLATION,
            RestrictionState.PENDING_CANCELLATION   => RestrictionState.CANCELLATION_APPROVED,
            RestrictionState.CANCELLATION_APPROVED       => RestrictionState.OUT_OF_FORCE,
            RestrictionState.OUT_OF_FORCE => RestrictionState.AWAITING_OPERATOR_SIGN_OUT,
            RestrictionState.AWAITING_OPERATOR_SIGN_OUT => RestrictionState.ARCHIVED,
            _ => throw new InvalidOperationException($"No next state after {opRes.State}.")
        };
        }
    }

    // calls the right requirement function and returns a bool - These should call the audit trail
    public bool Require(OperationalRestriction opRes)
    {
        switch (opRes.State)
        {
            case RestrictionState.ACTIVE:
                return ActiveStateRequirements(opRes);

            case RestrictionState.PENDING_CANCELLATION:
                return PendingCancellationStateRequirements(opRes);

            case RestrictionState.CANCELLATION_APPROVED:
                return CancellationApprovedStateRequirements(opRes);

            case RestrictionState.OUT_OF_FORCE:
                return OutOfForceStateRequirements(opRes);

            default:
                throw new InvalidOperationException("No matching state requirements found");
        }
    }

    public bool FindOneSignature(List<Signature> sheet, UserRole role, SignatureType type)
    {
        for (int i=0; i < sheet.Count; i++)
        {
            if (sheet[i].SigneeRole == role && sheet[i].Type == type)
            {
                if (!sheet[i].Signed)
                    return false;
                
                break;
            }
        }

        return true;
    }

    public bool FindAllSignatures(List<Signature> sheet, UserRole role, SignatureType type)
    {
        for (int i=0; i < sheet.Count; i++)
        {
            if (sheet[i].SigneeRole == role && sheet[i].Type == type)
            {
                if (!sheet[i].Signed)
                    return false;
            }
        }

        return true;
    }

    // Functions for each state that checks the requirements needed to transition
    public bool ActiveStateRequirements(OperationalRestriction opRes)
    {

        if (!FindOneSignature(opRes.SignatureSheet, UserRole.ORIGINATOR, SignatureType.CLOSURE))
        {
            throw new InvalidOperationException("Originator cancellation signiture missing");
        }

        return true;
    }

    public bool PendingCancellationStateRequirements(OperationalRestriction opRes)
    {
        if (!FindOneSignature(opRes.SignatureSheet, UserRole.DUTY_OPERATIONS_MANAGER, SignatureType.CLOSURE))
        {
            throw new InvalidOperationException("DOM cancellation signiture missing");
        }

        return true;
    }

    public bool CancellationApprovedStateRequirements(OperationalRestriction opRes)
    {
        if (!FindOneSignature(opRes.SignatureSheet, UserRole.CONTROLROOM_SUPERVISOR, SignatureType.CLOSURE))
        {
            throw new InvalidOperationException("CRS cancellation signiture missing");
        }

        return true;
    }

    public bool OutOfForceStateRequirements(OperationalRestriction opRes)
    {

    if (!FindAllSignatures(opRes.SignatureSheet, UserRole.OPERATOR, SignatureType.CLOSURE))
        {
            throw new InvalidOperationException("Originator cancellation signiture missing");
        }
    
        return true;

    }


}