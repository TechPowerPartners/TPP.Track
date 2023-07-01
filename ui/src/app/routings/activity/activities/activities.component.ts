import { Component, OnInit, OnDestroy, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject, catchError, of, takeUntil } from 'rxjs';
import {
  ActivityVm,
  ICreateActivityDto,
  IUpdateActivityDto,
} from 'src/service-proxies/dto/activity';
import { ActivityServiceProxy } from 'src/service-proxies/services/activity.service';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.scss'],
})
export class ActivitiesComponent implements OnInit, OnDestroy {
  public activities: ActivityVm[] = [];
  public modalRef?: BsModalRef;
  public activityForm: FormGroup;
  public activityId: string | null = null;

  private subject$: Subject<void> = new Subject<void>();

  constructor(
    private readonly _activityService: ActivityServiceProxy,
    private readonly _toastrService: ToastrService,
    private readonly _modalService: BsModalService,
    private readonly _fb: FormBuilder
  ) {
    this.activityForm = _fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
    });
  }

  public get nameControl(): AbstractControl {
    return this.activityForm.get('name') as AbstractControl;
  }

  public get descriptionControl(): AbstractControl {
    return this.activityForm.get('description') as AbstractControl;
  }

  ngOnInit(): void {
    this.initTable();
  }

  ngOnDestroy(): void {
    this.subject$.next();
    this.subject$.complete();
  }

  public closeModal(): void {
    this.modalRef?.hide();
  }

  public openModal(
    template: TemplateRef<any>,
    activityId: string | null = null
  ): void {
    if (activityId) {
      this.activityForm.patchValue({
        ...this.activities.find((a) => a.id === activityId),
      });
    }

    this.modalRef = this._modalService.show(template);
    this.modalRef.onHide?.subscribe(() => {
      this.activityForm.reset();
    });

    this.activityId = activityId;
  }

  public save(): void {
    if (!this.activityForm.valid) {
      return;
    }

    if (!this.activityId) {
      let createActivityDto =
        this.activityForm.getRawValue() as ICreateActivityDto;

      this._activityService.create(createActivityDto).subscribe(() => {
        this._toastrService.success('Активность успешно создана', 'Успешно');
        this.modalRef?.hide();
        this.initTable();
      });

      return;
    }

    let updateActivityDto =
      this.activityForm.getRawValue() as IUpdateActivityDto;

    updateActivityDto.id = this.activityId;

    this._activityService.update(updateActivityDto).subscribe(() => {
      this._toastrService.success('Активность успешно сохранена', 'Успешно');
      this.modalRef?.hide();
      this.initTable();
    });
  }

  public delete(id: string): void {
    this._activityService.delete(id).subscribe(() => {
      this._toastrService.success('Активность успешно удалена', 'Успешно');
      this.initTable();
    });
  }

  public initTable(): void {
    this._activityService
      .getAll()
      .pipe(
        catchError((error) => {
          this._toastrService.error(error);
          return of();
        }),
        takeUntil(this.subject$)
      )
      .subscribe((activities: ActivityVm[]) => {
        this.activities = activities;
      });
  }
}
